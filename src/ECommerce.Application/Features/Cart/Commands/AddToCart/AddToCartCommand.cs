namespace ECommerce.Application.Features.Cart.Commands.AddToCart;

public record AddToCartCommand(
    long ProductId,
    int Quantity
) : IRequest<Result>;