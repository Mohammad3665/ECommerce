namespace ECommerce.Application.Features.Coupons.Commands.ToggleCouponStatus;

public class ToggleCouponStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleCouponStatusCommand, Result>
{
    public async Task<Result> Handle(ToggleCouponStatusCommand request, CancellationToken cancellationToken)
    {
        var coupon = await unitOfWork.CouponRepository.GetAsync(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (coupon is null)
            return new Error("Coupon.NotFound", "کد تخفیف یافت نشد.", ErrorType.NotFound);

        coupon.IsActive = request.IsActive;

        unitOfWork.CouponRepository.Update(coupon);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Coupon.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
