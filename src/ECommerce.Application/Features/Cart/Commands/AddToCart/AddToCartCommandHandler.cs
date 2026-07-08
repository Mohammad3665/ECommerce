namespace ECommerce.Application.Features.Cart.Commands.AddToCart;

public class AddToCartCommandHandler(IUnitOfWork unitOfWork, ICartService cartService, ICurrentUserService currentUser) : IRequestHandler<AddToCartCommand, Result>
{
    public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken)
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

        var product = await unitOfWork.ProductRepository.GetAsync(
            expression: p => p.Id == request.ProductId,
            cancellationToken: cancellationToken
        );
        if (product is null)
        {
            var error = new Error(
                "Product.NotFound",
                "محصول مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var item = new RedisCartItemDto(
            ProductId: request.ProductId,
            ProductName: product.Name,
            Quantity: request.Quantity,
            UnitPrice: product.Price,
            TotalPrice: request.Quantity * product.Price
        );

        await cartService.AddItemAsync((Guid)userId, item);
        return Result.Success();
    }
}