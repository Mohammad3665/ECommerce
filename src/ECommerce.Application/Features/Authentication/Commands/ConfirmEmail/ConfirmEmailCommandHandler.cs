using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ConfirmEmailCommand, Result>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            var error = new Error(
                "Auth.UserNotFound",
                "No user found with this email address.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        if (user.SecurityCode != request.SecurityCode || user.SecurityCodeExpiry <= DateTime.UtcNow)
        {
            var error = new Error(
                "Auth.InvalidSecurityCode", 
                "Refresh token is invalid or expired.", 
                ErrorType.Forbidden
            );
            return Result.Failure(error);
        }
        user.IsActive = true;
        user.IsEmailConfirmed = true;

        user.SecurityCode = null;
        user.SecurityCodeExpiry = null;

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

}
