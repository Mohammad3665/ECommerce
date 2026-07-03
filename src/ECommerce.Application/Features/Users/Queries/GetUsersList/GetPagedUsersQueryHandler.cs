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
        var pagedResult = await unitOfWork.UserRepository.GetPagedListAsync<PagedUsersResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );

        return Result<Pagination<PagedUsersResponseDto>>.Success(pagedResult);
    }
}