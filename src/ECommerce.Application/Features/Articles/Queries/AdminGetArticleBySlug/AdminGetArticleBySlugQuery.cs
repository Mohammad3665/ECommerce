using ECommerce.Application.Dtos.Articles;

namespace ECommerce.Application.Features.Articles.Queries.AdminGetArticleBySlug;

public record AdminGetArticleBySlugQuery(string Slug) : IRequest<Result<GetAdminArticleResponseDto>>;
