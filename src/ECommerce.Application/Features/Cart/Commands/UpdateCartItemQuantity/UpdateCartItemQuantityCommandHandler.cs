namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public class UpdateCartItemQuantityCommandHandler(ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<UpdateCartItemQuantityCommand, Result>
{
    public async Task<Result> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
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

        await cartService.UpdateQuantityAsync((Guid)userId, request.ProductId, request.Quantity);
        return Result.Success();
    }
}