using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Application.Article;
using Mapster;

namespace ECommerce.Application.Features.ArticleCategories.Commands.EditArticleCategory;

public class EditArticleCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditArticleCategoryCommand, Result>
{
    public async Task<Result> Handle(EditArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await unitOfWork.ArticleCategoryRepository.GetAsync(
            expression: ac => ac.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (articleCategory is null)
        {
            var error = new Error(
                "ArticleCategory.NotFound",
                "دسته‌بندی یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        articleCategory.Slug = request.EnglishName.ToSlug();
        var config = new TypeAdapterConfig();
        config.NewConfig<EditArticleCategoryCommand, ArticleCategory>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Slug);

        request.Adapt(articleCategory, config);

        unitOfWork.ArticleCategoryRepository.Update(articleCategory);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "ArticleCategory.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        return Result.Success();
    }
}