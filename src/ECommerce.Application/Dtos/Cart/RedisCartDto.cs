namespace ECommerce.Application.Features.Cart;

public record RedisCartDto(
    Guid UserId,
    List<RedisCartItemDto> Items,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    decimal TotalPrice
);