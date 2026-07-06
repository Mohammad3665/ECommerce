namespace ECommerce.Application.Features.Users.Commands.ToggleUserStatus;

public class ToggleUserStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleUserStatusCommand, Result>
{
    public async Task<Result> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetAsync(
            expression: u => u.Id == request.UserId,
            cancellationToken: cancellationToken
        );
        if (user is null)
        {
            var error = new Error(
                "User.NotFound",
                "کاربر مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        user.IsActive = request.IsActive;

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
