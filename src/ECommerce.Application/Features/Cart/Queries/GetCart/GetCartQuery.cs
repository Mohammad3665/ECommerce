namespace ECommerce.Application.Features.Cart.Queries.GetCart;

public record GetCartQuery : IRequest<Result<RedisCartDto>>;