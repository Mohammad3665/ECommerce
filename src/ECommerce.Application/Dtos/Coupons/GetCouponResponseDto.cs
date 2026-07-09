namespace ECommerce.Application.Dtos.Coupons;

public record GetCouponResponseDto(
    Guid Id,
    string Code,
    string Type,
    decimal Value,
    decimal? MinOrderAmount,
    int? UsageLimit,
    int UsedCount,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive
);
