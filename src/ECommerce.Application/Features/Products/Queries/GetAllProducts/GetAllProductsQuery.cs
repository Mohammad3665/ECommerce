using ECommerce.Application.Dtos.Products;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<Result<IEnumerable<GetAllProductsResponseDto>>>;
