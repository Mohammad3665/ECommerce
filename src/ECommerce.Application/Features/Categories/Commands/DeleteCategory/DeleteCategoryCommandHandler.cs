using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(
                expression: c => c.Slug == request.Slug.Trim().ToLower(),
                cancellationToken: cancellationToken
            );

        if (category is null)
        {
            var error = new Error(
                "Category.NotFound",
                "دسته‌بندی یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var hasProduct = await unitOfWork.ProductRepository.IsExistAsync(
            expression: p => p.CategoryId == category.Id,
            cancellationToken: cancellationToken
        );
        if (hasProduct)
        {
            var error = new Error(
                "Category.CannotDeleteWithProducts",
                "این دسته‌بندی شامل محصول است و قابل حذف نیست. ابتدا محصولات را حذف نموده یا به دسته‌بندی دیگری انتقال دهید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        string? imagePathToDelete = category.ImageUrl;

        unitOfWork.CategoryRepository.DeletePermanently(category);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Category.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }
        if (!string.IsNullOrEmpty(imagePathToDelete))
        {
            fileService.DeleteFile(imagePathToDelete);
        }
        return Result.Success();
    }
}