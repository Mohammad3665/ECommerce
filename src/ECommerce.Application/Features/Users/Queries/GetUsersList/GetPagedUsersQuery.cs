using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUsersList;

public record GetPagedUsersQuery(
    int PageNumber,
    int PageSize,
    string? SearchTerm) : IRequest<Result<Pagination<PagedUsersResponseDto>>>;
