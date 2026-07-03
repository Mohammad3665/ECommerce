using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Application.Article;
using MediatR;

namespace ECommerce.Application.Features.ArticleCategories.Commands.CreateArticleCategory;

public record CreateArticleCategoryCommand(
    string Name,
    string EnglishName
) : IMapTo<ArticleCategory>, IRequest<Result<long>>;
