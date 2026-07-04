using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Application.Article;
using MediatR;

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
