using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Product;
using Mapster;

namespace ECommerce.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IUnitOfWork unitOfWork, ISlugService slugService) : IRequestHandler<CreateProductCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await unitOfWork.CategoryRepository.IsExistAsync(
            expression: c => c.Id == request.CategoryId,
            cancellationToken: cancellationToken
        );
        if (!categoryExists)
            return new Error("Product.CategoryNotFound", "دسته‌بندی انتخاب شده در سیستم یافت نشد.", ErrorType.Validation);

        var brandExists = await unitOfWork.BrandRepository.IsExistAsync(
            expression: b => b.Id == request.BrandId,
            cancellationToken: cancellationToken
        );
        if (!brandExists)
            return new Error("Product.BrandNotFound", "برند انتخاب شده در سیستم یافت نشد.", ErrorType.Validation);

        var isNameDuplicate = await unitOfWork.ProductRepository.IsExistAsync(
            expression: p => p.Name.ToLower() == request.Name.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isNameDuplicate)
            return new Error("Product.DuplicateName", "محصولی با این نام قبلاً ثبت شده است.", ErrorType.Validation);


        var product = request.Adapt<Product>();

        product.Slug = await slugService.GenerateProductSlugAsync(request.EnglishName, product.Id);
        product.Name = request.Name.Trim();
        product.EnglishName = request.EnglishName.Trim();
        product.ViewCount = 0;

        product.Specifications = request.Specifications
            .Select(s => new ProductSpecification
            {
                Key = s.Key.NormalizePersian(),
                Value = s.Value.NormalizePersian()
            })
            .ToList();

        product.Images = request.Images
            .Select(img => new ProductImage
            {
                ImageUrl = img.ImageUrl,
                IsMain = img.IsMain,
                DisplayOrder = img.DisplayOrder
            })
            .ToList();

        await unitOfWork.ProductRepository.AddAsync(product, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Product.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            product.Id;
    }
}