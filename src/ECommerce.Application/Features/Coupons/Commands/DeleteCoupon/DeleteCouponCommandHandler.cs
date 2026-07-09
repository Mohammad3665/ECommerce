namespace ECommerce.Application.Features.Coupons.Commands.DeleteCoupon;

public class DeleteCouponCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCouponCommand, Result>
{
    public async Task<Result> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await unitOfWork.CouponRepository.GetAsync(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (coupon is null)
            return new Error("Coupon.NotFound", "کد تخفیف یافت نشد.", ErrorType.NotFound);

        if (coupon.UsedCount > 0)
            return new Error("Coupon.HasBeenUsed", "این کد تخفیف قبلاً استفاده شده و قابل حذف نیست.", ErrorType.Validation);

        unitOfWork.CouponRepository.DeletePermanently(coupon);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Coupon.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
