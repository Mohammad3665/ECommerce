namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler(ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<RemoveFromCartCommand, Result>
{
    public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
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

        await cartService.RemoveItemAsync((Guid)userId, request.ProductId);
        return Result.Success();
    }
}