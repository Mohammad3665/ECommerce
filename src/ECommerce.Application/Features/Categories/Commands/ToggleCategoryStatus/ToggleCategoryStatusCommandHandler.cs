namespace ECommerce.Application.Features.Categories.Commands.ToggleCategoryStatus;

public class ToggleCategoryStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleCategoryStatusCommand, Result>
{
    public async Task<Result> Handle(ToggleCategoryStatusCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(
            expression: c => c.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (category is null)
            return new Error("Category.NotFound", "دسته‌بندی مورد نظر یافت نشد.", ErrorType.NotFound);

        category.IsActive = request.IsActive;

        unitOfWork.CategoryRepository.Update(category);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Category.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
