namespace ECommerce.Application.Dtos.Coupons;

public record CreateCouponRequestDto(
    string Code,
    string Type,
    decimal Value,
    decimal? MinOrderAmount,
    int? UsageLimit,
    DateTime StartDate,
    DateTime EndDate
);