namespace ECommerce.Application.Features.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler(IUnitOfWork unitOfWork, IEmailService emailService, ICodeGeneratorService codeGenerator) : IRequestHandler<ForgotPasswordCommand, Result>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByEmailAsync(request.Email, cancellationToken);
        if (user is null)
        {
            var error = new Error(
                "Auth.UserNotFound",
                "کاربری با این ایمیل یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var securityCode = codeGenerator.Generate();

        user.SecurityCode = securityCode;
        user.SecurityCodeExpiry = DateTime.UtcNow.AddHours(1);

        unitOfWork.UserRepository.Update(user);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Auth.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        var subject = "بازیابی رمز عبور - فروشگاه من";
        var emailBody = $"<h3>کد تایید شما:</h3><h1 style='color:blue;'>{securityCode}</h1><p>این کد تا 60 دقیقه دیگر معتبر است.</p>";
        await emailService.SendEmailAsync(user.Email, subject, emailBody);

        return Result.Success();
    }

}
