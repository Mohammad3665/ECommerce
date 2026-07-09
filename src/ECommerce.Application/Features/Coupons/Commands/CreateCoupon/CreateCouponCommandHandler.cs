using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.Enums;
using Mapster;

namespace ECommerce.Application.Features.Coupons.Commands.CreateCoupon;

public class CreateCouponCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCouponCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var isDuplicateCode = await unitOfWork.CouponRepository.IsExistAsync(
            expression: c => c.Code.ToLower() == request.Code.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isDuplicateCode)
            return new Error("Coupon.DuplicateCode", "کد تخفیف تکراری است.", ErrorType.Validation);

        if (!Enum.TryParse<CouponType>(request.Type, out var couponType))
            return new Error("Coupon.InvalidType", "نوع تخفیف نامعتبر است.", ErrorType.Validation);

        var coupon = request.Adapt<Coupon>();
        coupon.Code = request.Code.Trim().ToUpper();
        coupon.Type = couponType;
        coupon.UsedCount = 0;


        await unitOfWork.CouponRepository.AddAsync(coupon, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Coupon.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            coupon.Id;
    }
}