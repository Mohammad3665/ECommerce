namespace ECommerce.Application.Features.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return new Error("Auth.UserNotFound", "کاربر یافت نشد.", ErrorType.NotFound);

        var isCurrentPasswordValid = passwordService.Verify(request.CurrentPassword, user.PasswordHash);
        if (!isCurrentPasswordValid)
            return new Error("Auth.InvalidCurrentPassword", "پسورد فعلی وارد شده صحیح نیست.", ErrorType.Validation);

        var newHashedPassword = passwordService.Hash(request.NewPassword);

        user.PasswordHash = newHashedPassword;
        user.SecurityCode = null;
        user.SecurityCodeExpiry = null;

        unitOfWork.UserRepository.Update(user);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Auth.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
