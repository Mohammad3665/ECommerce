namespace ECommerce.Application.Features.Articles.Commands.EditArticle;

public record EditArticleCommand(
    string Slug,
    string Title,
    string EnglishTitle,
    string Content,
    string Summary,
    long ArticleCategoryId,
    string? ImageUrl
) : IRequest<Result>;