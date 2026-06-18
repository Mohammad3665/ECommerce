using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            var error = new Error(
                "Auth.InvalidResetRequest",
                "Invalid email or security code.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        if (user.SecurityCode is null || user.SecurityCode != request.SecurityCode || user.SecurityCodeExpiry <= DateTime.UtcNow)
        {
            var error = new Error(
                "Auth.InvalidOrExpiredCode",
                "The security code is invalid or has expired.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        var newHashedPassword = passwordService.Hash(request.NewPassword);

        user.PasswordHash = newHashedPassword;
        user.SecurityCode = null;
        user.SecurityCodeExpiry = null;

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
