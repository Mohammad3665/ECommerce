namespace ECommerce.Application.Dtos.Coupons;

public record GetAllCouponsResponseDto(
    Guid Id,
    string Code,
    string Type,
    decimal Value,
    decimal? MinOrderAmount,
    bool IsActive
);