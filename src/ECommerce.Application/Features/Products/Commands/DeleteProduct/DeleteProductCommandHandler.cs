using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.Slug == request.Slug.Trim().ToLower(),
            includes: query => query.Images,
            cancellationToken: cancellationToken
        );
        if (product is null)
        {
            var error = new Error(
                "Product.NotFound",
                "محصول مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var imagePathsToRemove = product.Images
            .Select(img => img.ImageUrl)
            .Where(url => !string.IsNullOrWhiteSpace(url))
            .ToList();

        unitOfWork.ProductRepository.DeletePermanently(product);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Product.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        foreach (var relativeUrl in imagePathsToRemove)
        {
            fileService.DeleteFile(relativeUrl);
        }

        return Result.Success();
    }
}
