namespace ECommerce.Application.Dtos.Orders;

public record OrderItemDto(
    string ProductName,
    decimal UnitPrice,
    int Quantity,
    decimal TotalPrice
);