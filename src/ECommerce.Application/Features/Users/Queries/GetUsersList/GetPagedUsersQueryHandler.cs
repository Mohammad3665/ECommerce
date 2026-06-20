using System.Linq.Expressions;
using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using ECommerce.Domain.Specifications.Common;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUsersList;

public class GetPagedUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedUsersQuery, Result<Pagination<PagedUsersResponseDto>>>
{
    public async Task<Result<Pagination<PagedUsersResponseDto>>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<User, bool>>? filterExpression = null;
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var search = request.SearchTerm.Trim().ToLower();
            filterExpression = c => c.FullName.ToLower().Contains(search);
        }
        var pagedResult = await unitOfWork.UserRepository.GetAllAsync(
            current: request.PageNumber,
            take: request.PageSize,
            selector: src => src.Adapt<PagedUsersResponseDto>(),
            expression: filterExpression,
            order: o => o.OrderByDescending(c => c.Id),
            cancellationToken: cancellationToken
        );

        return Result<Pagination<PagedUsersResponseDto>>.Success(pagedResult);
    }

}
