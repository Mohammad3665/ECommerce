namespace ECommerce.Application.Features.Cart.Queries.GetCart;

public class GetCartQueryHandler(ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<GetCartQuery, Result<RedisCartDto>>
{
    public async Task<Result<RedisCartDto>> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        var userId = currentUser.UserId.Value;
        var cart = await cartService.GetCartAsync(userId);
        return cart;
    }
}
