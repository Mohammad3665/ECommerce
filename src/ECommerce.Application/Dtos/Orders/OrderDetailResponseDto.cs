using System.Text.Json.Serialization;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Dtos.Orders;

public record OrderDetailResponseDto(
    long OrderId,
    string OrderNumber,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    OrderStatus Status,
    DateTime OrderDate,
    decimal SubTotal,
    decimal DiscountAmount,
    decimal ShippingCost,
    decimal TotalAmount,
    List<OrderItemDto> Items,
    OrderShippingDto Shipping,
    List<OrderHistoryDto> History
);