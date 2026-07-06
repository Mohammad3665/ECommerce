using ECommerce.Domain.Entities.Application.Article;

namespace ECommerce.Application.Features.ArticleCategories.Commands.CreateArticleCategory;

public record CreateArticleCategoryCommand(
    string Name,
    string EnglishName
) : IMapTo<ArticleCategory>, IRequest<Result<long>>;
