using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<Result<IEnumerable<GetAllProductsResponseDto>>>;
