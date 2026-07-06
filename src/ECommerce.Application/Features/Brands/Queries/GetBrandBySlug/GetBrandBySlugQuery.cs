using ECommerce.Application.Dtos.Brands;

namespace ECommerce.Application.Features.Brands.Queries.GetBrandBySlug;

public record GetBrandBySlugQuery(string Slug) : IRequest<Result<GetBrandResponseDto>>;
