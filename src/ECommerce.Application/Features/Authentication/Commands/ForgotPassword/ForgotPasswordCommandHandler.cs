using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ForgotPasswordCommand, Result>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
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

        var random = new Random();
        var securityCode = random.Next(100000, 999999).ToString();

        user.SecurityCode = securityCode;
        user.SecurityCodeExpiry = DateTime.UtcNow.AddMinutes(5);

        unitOfWork.UserRepository.Update(user);
        await unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }

}
