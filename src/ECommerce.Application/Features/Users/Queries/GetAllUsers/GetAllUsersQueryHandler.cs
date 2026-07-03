using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<GetAllUsersResponseDto>>>
{
    public async Task<Result<IEnumerable<GetAllUsersResponseDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetAllAsync<GetAllUsersResponseDto>(
            expression: null,
            order: query => query.OrderBy(u => u.FullName),
            cancellationToken: cancellationToken
        );

        return Result<IEnumerable<GetAllUsersResponseDto>>.Success(users);
    }
}
