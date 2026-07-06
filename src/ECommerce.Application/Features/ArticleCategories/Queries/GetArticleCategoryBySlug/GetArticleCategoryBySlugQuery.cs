using ECommerce.Application.Dtos.ArticleCategories;

namespace ECommerce.Application.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;

public record GetArticleCategoryBySlugQuery(string Slug) : IRequest<Result<GetArticleCategoryResponseDto>>;
