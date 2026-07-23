using ECommerce.Domain.Entities.Product.Events.Category;
using Microsoft.EntityFrameworkCore;

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
            return new Error("Category.NotFound", "دسته‌بندی یافت نشد.", ErrorType.NotFound);

        var hasProduct = await unitOfWork.ProductRepository.IsExistAsync(
            expression: p => p.CategoryId == category.Id,
            cancellationToken: cancellationToken
        );
        if (hasProduct)
            return new Error("Category.CannotDeleteWithProducts", "این دسته‌بندی شامل محصول است و قابل حذف نیست. ابتدا محصولات را حذف نموده یا به دسته‌بندی دیگری انتقال دهید.", ErrorType.Validation);

        string? imagePathToDelete = category.ImageUrl;

        unitOfWork.CategoryRepository.DeletePermanently(category);

        if (!string.IsNullOrEmpty(imagePathToDelete))
            category.AddDomainEvent(new CategoryDeletedDomainEvent(imagePathToDelete));

        try
        {
            var saveResult = await unitOfWork.SaveAsync(cancellationToken);
            if (saveResult.IsFailure)
                return new Error("Category.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);
        }
        catch (DbUpdateException)
        {
            return new Error("Category.HasDependencies", "این دسته‌بندی دارای وابستگی‌هایی است و قابل حذف نیست.", ErrorType.Validation);
        }

        return Result.Success();
    }
}
