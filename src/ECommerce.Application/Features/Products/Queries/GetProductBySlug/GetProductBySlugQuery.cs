using ECommerce.Application.Dtos.Products;

namespace ECommerce.Application.Features.Products.Queries.GetProductBySlug;

public record GetProductBySlugQuery(string Slug) : IRequest<Result<GetProductResponseDto>>;
