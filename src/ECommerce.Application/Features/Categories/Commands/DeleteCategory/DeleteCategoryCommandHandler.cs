using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, Result>
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
                $"دسته‌بندی یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.CategoryId == category.Id,
            cancellationToken: cancellationToken
        );
        if (product is not null)
        {
            var error = new Error(
                "Category.CannotDeleteWithProducts",
                "این دسته‌بندی شامل محصول است و قابل حذف نیست. ابتدا محصولات را حذف نموده یا به دسته‌بندی دیگری انتقال دهید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

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
        return Result.Success();
    }
}