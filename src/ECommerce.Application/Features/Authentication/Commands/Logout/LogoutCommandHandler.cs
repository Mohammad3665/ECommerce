namespace ECommerce.Application.Features.Authentication.Commands.Logout;

public class LogoutCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<LogoutCommand, Result>
{
    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRolesByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            var error = new Error(
                "Auth.UserNotFound",
                "کاربر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;

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
