using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.Events.Category;
using Mapster;

namespace ECommerce.Application.Features.Categories.Commands.EditCategory;

public class EditCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditCategoryCommand, Result>
{
    public async Task<Result> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(
            expression: c => c.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (category is null)
            return new Error("Category.NotFound", "دسته‌بندی یافت نشد.", ErrorType.NotFound);

        string? oldImagePath = category.ImageUrl;
        var hasNewImage = request.ImageUrl != null;

        category.Slug = request.EnglishName.ToSlug();
        var config = new TypeAdapterConfig();
        config.NewConfig<EditCategoryCommand, Category>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Slug)
            .Ignore(dest => dest.ImageUrl!);
        request.Adapt(category, config);

        if (hasNewImage)
            category.ImageUrl = request.ImageUrl;

        unitOfWork.CategoryRepository.Update(category);

        if (hasNewImage && !string.IsNullOrEmpty(oldImagePath))
            category.AddDomainEvent(new CategoryEditedDomainEvent(oldImagePath));

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
            return new Error("Category.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);

        return Result.Success();
    }
}
