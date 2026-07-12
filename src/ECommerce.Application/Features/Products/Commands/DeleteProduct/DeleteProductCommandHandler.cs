using ECommerce.Domain.Events.Product;

namespace ECommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.Slug == request.Slug.Trim().ToLower(),
            includes: query => query.Images,
            cancellationToken: cancellationToken
        );
        if (product is null)
            return new Error("Product.NotFound", "محصول مورد نظر یافت نشد.", ErrorType.NotFound);

        var imagePathsToRemove = product.Images
            .Select(img => img.ImageUrl)
            .Where(url => !string.IsNullOrWhiteSpace(url))
            .ToList();

        unitOfWork.ProductRepository.DeletePermanently(product);

        product.AddDomainEvent(new ProductDeletedDomainEvent(imagePathsToRemove));

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
            return new Error("Product.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);

        return Result.Success();
    }
}