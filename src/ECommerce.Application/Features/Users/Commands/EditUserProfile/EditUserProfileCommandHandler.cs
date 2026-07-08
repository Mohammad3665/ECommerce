namespace ECommerce.Application.Features.Users.Commands.EditUserProfile;

public class EditUserProfileCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<EditUserProfileCommand, Result>
{
    public async Task<Result> Handle(EditUserProfileCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        var user = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Id == currentUser.UserId.Value,
            cancellationToken: cancellationToken
        );
        if (user is null)
            return new Error("User.NotFound", "حساب کاربری یافت نشد.", ErrorType.NotFound);

        user.FullName = request.FullName;
        user.PhoneNumber = request.PhoneNumber;

        unitOfWork.UserRepository.Update(user);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("User.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
