using ECommerce.Domain.Entities.Product;
using Mapster;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isDuplicateEnglishName = await unitOfWork.CategoryRepository.IsExistAsync(
            expression: c => c.EnglishName.ToLower() == request.EnglishName.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isDuplicateEnglishName)
            return new Error("Category.DuplicateName", "دسته‌بندی با این نام انگلیسی قبلا در سیستم ثبت شده است.", ErrorType.Validation);

        var category = request.Adapt<Category>();

        await unitOfWork.CategoryRepository.AddAsync(category, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Category.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            category.Id;
    }
}
