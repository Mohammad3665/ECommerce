namespace ECommerce.Application.Features.Coupons.Commands.DeleteCoupon;

public record DeleteCouponCommand(Guid Id) : IRequest<Result>;
