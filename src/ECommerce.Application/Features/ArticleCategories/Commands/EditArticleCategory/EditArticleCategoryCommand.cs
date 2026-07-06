namespace ECommerce.Application.Features.ArticleCategories.Commands.EditArticleCategory;

public record EditArticleCategoryCommand(
    string Slug,
    string Name,
    string EnglishName
) : IRequest<Result>;
