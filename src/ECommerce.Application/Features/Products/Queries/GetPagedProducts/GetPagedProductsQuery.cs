using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetPagedProducts;

public class GetPagedProductsQuery : QueryRequest, IRequest<Result<Pagination<GetPagedProductsResponseDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
}