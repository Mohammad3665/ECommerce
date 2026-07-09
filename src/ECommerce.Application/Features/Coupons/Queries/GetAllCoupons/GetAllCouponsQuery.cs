using ECommerce.Application.Dtos.Coupons;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Coupons.Queries.GetAllCoupons;

public class GetAllCouponsQuery : QueryRequest, IRequest<Result<Pagination<GetAllCouponsResponseDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
}