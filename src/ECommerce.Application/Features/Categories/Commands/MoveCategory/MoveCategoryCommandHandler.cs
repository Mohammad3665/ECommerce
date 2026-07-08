namespace ECommerce.Application.Features.Categories.Commands.MoveCategory;

public class MoveCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<MoveCategoryCommand, Result>
{
    public async Task<Result> Handle(MoveCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(
            expression: c => c.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (category is null)
            return new Error("Category.NotFound", "دسته‌بندی یافت نشد.", ErrorType.NotFound);

        if (request.NewParentId.HasValue)
        {
            var newParentId = request.NewParentId.Value;

            if (category.Id == newParentId)
                return new Error("Category.SelfParenting", "یک دسته‌بندی نمی‌تواند به عنوان والد خودش انتخاب شود.", ErrorType.Validation);

            var parentExists = await unitOfWork.CategoryRepository.IsExistAsync(
                expression: c => c.Id == newParentId,
                cancellationToken: cancellationToken
            );

            var isDecendant = await IsTargetParentADescendantAsync(category.Id, newParentId, cancellationToken);
            if (isDecendant)
                return new Error("Category.CircularReference", "انتقال دسته‌بندی به زیرمجموعه‌های خودش غیرمجاز است، چون باعث ایجاد حلقه بی‌انتها می‌شود.", ErrorType.Validation);
        }

        category.ParentCategoryId = request.NewParentId;

        unitOfWork.CategoryRepository.Update(category);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Category.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }

    private async Task<bool> IsTargetParentADescendantAsync(long currentCategoryId, long targetParentId, CancellationToken cancellationToken = default)
    {
        var checkCategory = await unitOfWork.CategoryRepository.GetAsync(
            expression: c => c.Id == targetParentId,
            cancellationToken: cancellationToken
        );

        while (checkCategory is not null)
        {
            if (checkCategory.ParentCategoryId == currentCategoryId)
                return true;

            if (checkCategory.ParentCategoryId is null)
                break;

            checkCategory = await unitOfWork.CategoryRepository.GetAsync(
                expression: c => c.Id == checkCategory.ParentCategoryId,
                cancellationToken: cancellationToken
            );
        }
        return false;
    }
}
