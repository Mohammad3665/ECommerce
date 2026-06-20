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
                "Auth.UserNotFound",
                "کاربر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var isCurrentPasswordValid = passwordService.Verify(request.CurrentPassword, user.PasswordHash);
        if (!isCurrentPasswordValid)
        {
            var error = new Error(
                "Auth.InvalidCurrentPassword", 
                "پسورد فعلی وارد شده صحیح نیست.", 
                ErrorType.Validation);
            
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
