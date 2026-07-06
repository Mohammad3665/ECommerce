using ECommerce.Application.Dtos.ArticleCategories;

namespace ECommerce.Application.Features.ArticleCategories.Queries.GetAllArticleCategories;

public record GetAllArticleCategoriesQuery : IRequest<Result<IEnumerable<GetArticleCategoryResponseDto>>>;
