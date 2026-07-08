namespace ECommerce.Application.Features.Authentication.Commands.ResetForgotedPassword;

public class ResetForgotedPasswordCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService) : IRequestHandler<ResetForgotedPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetForgotedPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return new Error("Auth.InvalidResetRequest", "ایمیل یا کد امنیتی نامعتبر است.", ErrorType.Validation);

        if (user.SecurityCode is null || user.SecurityCode != request.SecurityCode || user.SecurityCodeExpiry <= DateTime.UtcNow)
            return new Error("Auth.InvalidOrExpiredCode", "کد امنیتی منفضی شده یا نامعتبر است.", ErrorType.Validation);

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
