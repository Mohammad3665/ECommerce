using ECommerce.Domain.Entities.Application.Article;

namespace ECommerce.Application.Features.Articles.Commands.CreateArticle;

public record CreateArticleCommand(
    string Title,
    string EnglishTitle,
    string Content,
    string Summary,
    string Status,
    long ArticleCategoryId,
    Guid AuthorId,
    string? ImageUrl
) : IMapTo<Article>, IRequest<Result<long>>;
