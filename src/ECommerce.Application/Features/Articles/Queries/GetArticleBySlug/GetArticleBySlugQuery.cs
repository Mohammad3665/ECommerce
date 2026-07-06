using ECommerce.Application.Dtos.Articles;

namespace ECommerce.Application.Features.Articles.Queries.GetArticleBySlug;

public record GetArticleBySlugQuery(string Slug) : IRequest<Result<GetArticleResponseDto>>;
