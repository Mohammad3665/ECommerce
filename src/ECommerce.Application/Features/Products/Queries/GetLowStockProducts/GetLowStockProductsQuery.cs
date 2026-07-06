using ECommerce.Application.Dtos.Products;

namespace ECommerce.Application.Features.Products.Queries.GetLowStockProducts;

public record GetLowStockProductsQuery : IRequest<Result<IEnumerable<LowStockProductDto>>>;
