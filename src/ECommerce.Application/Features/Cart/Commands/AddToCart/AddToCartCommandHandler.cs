namespace ECommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommandHandler(IUnitOfWork unitOfWork, ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<AddToCartCommand, Result>
{
    public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.Id == request.ProductId,
            cancellationToken: cancellationToken
        );
        if (product is null)
            return new Error("Product.NotFound", "محصول مورد نظر یافت نشد.", ErrorType.NotFound);

        var item = new RedisCartItemDto(
            ProductId: request.ProductId,
            ProductName: product.Name,
            Quantity: request.Quantity,
            UnitPrice: product.Price,
            TotalPrice: request.Quantity * product.Price
        );

        await cartService.AddItemAsync((Guid)currentUser.UserId, item);
        return Result.Success();
    }
}
