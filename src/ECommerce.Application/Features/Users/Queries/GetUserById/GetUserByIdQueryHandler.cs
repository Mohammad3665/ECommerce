using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery, Result<GetUserResponseDto>>
{
    public async Task<Result<GetUserResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetAsync<GetUserResponseDto>(
            expression: u => u.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (user is null)
        {
            var error = new Error(
                "User.NotFound",
                "حساب کاربری یافت نشد.",
                ErrorType.NotFound
            );
            return Result<GetUserResponseDto>.Failure(error);
        }

        return Result<GetUserResponseDto>.Success(user);
    }
}
