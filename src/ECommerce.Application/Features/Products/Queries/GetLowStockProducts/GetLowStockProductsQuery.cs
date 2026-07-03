using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetLowStockProducts;

public record GetLowStockProductsQuery : IRequest<Result<IEnumerable<LowStockProductDto>>>;
