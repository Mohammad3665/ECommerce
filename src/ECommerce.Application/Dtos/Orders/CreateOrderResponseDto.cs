namespace ECommerce.Application.Dtos.Orders;

public record CreateOrderResponseDto(
    long OrderId,
    string OrderNumber,
    decimal TotalAmount,
    DateTime OrderDate
);