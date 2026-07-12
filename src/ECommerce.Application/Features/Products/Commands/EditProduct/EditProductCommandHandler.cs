using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.Events.Product;
using Mapster;

namespace ECommerce.Application.Features.Products.Commands.EditProduct;

public class EditProductCommandHandler(IUnitOfWork unitOfWork, IProductImageService imageService, IProductSpecificationService specificationService, ISlugService slugService) : IRequestHandler<EditProductCommand, Result>
{
    public async Task<Result> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.Slug == request.CurrentSlug.Trim().ToLower(),
            cancellationToken: cancellationToken,
            includes: [
                query => query.Images,
                query => query.Specifications
            ]
        );
        if (product is null)
            return new Error("Product.NotFound", "محصول مورد نظر یافت نشد.", ErrorType.NotFound);

        if (!string.Equals(product.EnglishName, request.EnglishName.Trim(), StringComparison.OrdinalIgnoreCase))
            product.Slug = await slugService.GenerateProductSlugAsync(
                request.EnglishName,
                product.Id,
                cancellationToken);

        var config = new TypeAdapterConfig();
        config.NewConfig<EditProductCommand, Product>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Slug)
            .Ignore(dest => dest.Images)
            .Ignore(dest => dest.Specifications);

        request.Adapt(product, config);
        product.Name = request.Name.Trim();
        product.EnglishName = request.EnglishName.Trim();

        var deletedImages = await imageService.UpdateImagesAsync(product, request.Images, cancellationToken);
        await specificationService.SyncAsync(product, request.Specifications, cancellationToken);
        unitOfWork.ProductRepository.Update(product);

        product.AddDomainEvent(new ProductEditedDomainEvent(deletedImages));

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
            return new Error("Product.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);

        return Result.Success();
    }
}
