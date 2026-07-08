namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public class UpdateCartItemQuantityCommandHandler(ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<UpdateCartItemQuantityCommand, Result>
{
    public async Task<Result> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        await cartService.UpdateQuantityAsync((Guid)currentUser.UserId, request.ProductId, request.Quantity);
        return Result.Success();
    }
}
