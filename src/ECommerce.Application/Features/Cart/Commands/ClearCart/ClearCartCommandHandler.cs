namespace ECommerce.Application.Features.Cart.Commands.ClearCart;

public class ClearCartCommandHandler(ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<ClearCartCommand, Result>
{
    public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        await cartService.ClearCart((Guid)currentUser.UserId);
        return Result.Success();
    }
}
