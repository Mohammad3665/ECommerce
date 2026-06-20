using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Users.Commands.ToggleUserStatus;

public class ToggleUserStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleUserStatusCommand, Result>
{
    public async Task<Result> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Id == request.UserId,
            cancellationToken: cancellationToken
        );
        if (user is null)
        {
            var error = new Error(
                "User.NotFound", 
                "کاربر مورد نظر یافت نشد.", 
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        user.IsActive = request.IsActive;

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
