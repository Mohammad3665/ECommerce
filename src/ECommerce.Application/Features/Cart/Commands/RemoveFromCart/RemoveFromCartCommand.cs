namespace ECommerce.Application.Features.Cart.Commands.RemoveFromCart;

public record RemoveFromCartCommand(
    long ProductId
) : IRequest<Result>;
