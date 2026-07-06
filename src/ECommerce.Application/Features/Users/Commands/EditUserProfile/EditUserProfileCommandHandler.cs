namespace ECommerce.Application.Features.Users.Commands.EditUserProfile;

public class EditUserProfileCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<EditUserProfileCommand, Result>
{
    public async Task<Result> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUser.UserId;
        if (userId is null)
        {
            var error = new Error(
                "Auth.Unauthorized",
                "کاربر احراز هویت نشده است.",
                ErrorType.Unauthorized
            );
            return Result.Failure(error);
        }

        var user = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Id == userId.Value,
            cancellationToken: cancellationToken
        );
        if (user is null)
        {
            var error = new Error(
                "User.NotFound",
                "حساب کاربری یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        user.FullName = request.FullName;
        user.PhoneNumber = request.PhoneNumber;

        unitOfWork.UserRepository.Update(user);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "User.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        return Result.Success();
    }

}
