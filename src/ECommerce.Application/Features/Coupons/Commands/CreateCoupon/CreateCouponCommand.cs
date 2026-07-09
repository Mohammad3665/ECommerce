using ECommerce.Domain.Entities.Order;

namespace ECommerce.Application.Features.Coupons.Commands.CreateCoupon;

public record CreateCouponCommand(
    string Code,
    string Type,
    decimal Value,
    decimal? MinOrderAmount,
    int? UsageLimit,
    DateTime StartDate,
    DateTime EndDate
) : IRequest<Result<Guid>>, IMapTo<Coupon>;