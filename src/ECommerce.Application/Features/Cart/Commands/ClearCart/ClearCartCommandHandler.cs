namespace ECommerce.Application.Features.Cart.Commands.ClearCart;

public class ClearCartCommandHandler(ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<ClearCartCommand, Result>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
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

        await cartService.ClearCart((Guid)userId);
        return Result.Success();
    }
}