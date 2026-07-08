using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Users.Queries.GetPagedUsers;

public class GetPagedUsersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedUsersQuery, Result<Pagination<PagedUsersResponseDto>>>
{
    public async Task<Result<Pagination<PagedUsersResponseDto>>> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetPagedListAsync<PagedUsersResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );
        if (users is null)
            return new Error("User.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return users;
    }
}