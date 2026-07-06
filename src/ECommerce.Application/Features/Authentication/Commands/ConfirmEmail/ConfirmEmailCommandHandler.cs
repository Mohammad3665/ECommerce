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
                "کاربری با این ایمیل یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        if (user.SecurityCode != request.SecurityCode || user.SecurityCodeExpiry <= DateTime.UtcNow)
        {
            var error = new Error(
                "Auth.InvalidSecurityCode",
                "رفرش توکن منقضی شده یا نامعتبر است.",
                ErrorType.Forbidden
            );
            return Result.Failure(error);
        }
        user.IsActive = true;
        user.IsEmailConfirmed = true;

        user.SecurityCode = null;
        user.SecurityCodeExpiry = null;

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

        return Result.Success();
    }

}
