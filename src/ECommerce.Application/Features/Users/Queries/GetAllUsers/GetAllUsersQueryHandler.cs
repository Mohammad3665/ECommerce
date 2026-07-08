using ECommerce.Application.Dtos.Users;

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
        if (users is null || !users.Any())
            return new Error("User.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<GetAllUsersResponseDto>>.Success(users);
    }
}
