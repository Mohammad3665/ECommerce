using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUsersList;

public class GetPagedUsersQuery : QueryRequest, IRequest<Result<Pagination<PagedUsersResponseDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
}
