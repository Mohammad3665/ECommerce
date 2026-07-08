namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler(ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<RemoveFromCartCommand, Result>
{
    public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        await cartService.RemoveItemAsync((Guid)currentUser.UserId, request.ProductId);
        return Result.Success();
    }
}
