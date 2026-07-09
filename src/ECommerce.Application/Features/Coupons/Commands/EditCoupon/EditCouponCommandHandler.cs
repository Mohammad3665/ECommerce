using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.Enums;
using Mapster;

namespace ECommerce.Application.Features.Coupons.Commands.EditCoupon;

public class EditCouponCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditCouponCommand, Result>
{
    public async Task<Result> Handle(EditCouponCommand request, CancellationToken cancellationToken)
    {
        var coupon = await unitOfWork.CouponRepository.GetAsync(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (coupon is null)
            return new Error("Coupon.NotFound", "کد تخفیف یافت نشد.", ErrorType.NotFound);

        var isDuplicateCode = await unitOfWork.CouponRepository.IsExistAsync(
            expression: c => c.Code.ToLower() == request.Code.Trim().ToLower() && c.Id != request.Id,
            cancellationToken: cancellationToken
        );
        if (isDuplicateCode)
            return new Error("Coupon.DuplicateCode", "کد تخفیف تکراری است.", ErrorType.Validation);

        if (!Enum.TryParse<CouponType>(request.Type, out var couponType))
            return new Error("Coupon.InvalidType", "نوع تخفیف نامعتبر است.", ErrorType.Validation);

        coupon.Code = request.Code.Trim().ToUpper();
        coupon.Type = couponType;

        var config = new TypeAdapterConfig();
        config.NewConfig<EditCouponCommand, Coupon>()
            .IgnoreNullValues(true)
            .Ignore(c => c.Code)
            .Ignore(c => c.Type);
        request.Adapt(coupon, config);

        unitOfWork.CouponRepository.Update(coupon);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Coupon.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
