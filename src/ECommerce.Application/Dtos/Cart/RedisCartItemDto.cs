namespace ECommerce.Application.Features.Cart;

public record RedisCartItemDto(
    long ProductId,
    string ProductName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice
);