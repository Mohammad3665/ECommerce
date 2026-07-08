namespace ECommerce.Application.Features.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ConfirmEmailCommand, Result>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return new Error("Auth.UserNotFound", "کاربری با این ایمیل یافت نشد.", ErrorType.NotFound);

        if (user.SecurityCode != request.SecurityCode || user.SecurityCodeExpiry <= DateTime.UtcNow)
            return new Error("Auth.InvalidSecurityCode", "رفرش توکن منقضی شده یا نامعتبر است.", ErrorType.Forbidden);

        user.IsActive = true;
        user.IsEmailConfirmed = true;

        user.SecurityCode = null;
        user.SecurityCodeExpiry = null;

        unitOfWork.UserRepository.Update(user);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Auth.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
