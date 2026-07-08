using ECommerce.Domain.Enums;

namespace ECommerce.Application.Dtos.Orders;

public record OrderHistoryDto(
    OrderStatus Status,
    string Note,
    DateTime ChangedAt
);