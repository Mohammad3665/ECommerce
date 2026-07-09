namespace ECommerce.Application.Features.Coupons.Commands.ToggleCouponStatus;

public record ToggleCouponStatusCommand(Guid Id, bool IsActive) : IRequest<Result>;
