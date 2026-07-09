namespace ECommerce.Application.Features.Coupons.Commands.EditCoupon;

public record EditCouponCommand(
    Guid Id,
    string Code,
    string Type,
    decimal Value,
    decimal? MinOrderAmount,
    int? UsageLimit,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<Result>;
