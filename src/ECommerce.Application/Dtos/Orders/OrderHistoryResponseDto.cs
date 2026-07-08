using System.Text.Json.Serialization;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Dtos.Orders;

public record OrderHistoryResponseDto(
    long OrderId,
    string OrderNumber,
    [property: JsonConverter(typeof(JsonStringEnumConverter))]
    OrderStatus Status,
    decimal TotalAmount,
    DateTime OrderDate,
    int ItemCount
);
