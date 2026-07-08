namespace ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;

public record UpdateCartItemQuantityCommand(
    long ProductId,
    int Quantity
) : IRequest<Result>;
