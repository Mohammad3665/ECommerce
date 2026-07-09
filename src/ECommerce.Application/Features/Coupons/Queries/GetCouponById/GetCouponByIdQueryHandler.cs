using ECommerce.Application.Dtos.Coupons;

namespace ECommerce.Application.Features.Coupons.Queries.GetCouponById;

public class GetCouponByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCouponByIdQuery, Result<GetCouponResponseDto>>
{
    public async Task<Result<GetCouponResponseDto>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        var coupon = await unitOfWork.CouponRepository.GetAsync<GetCouponResponseDto>(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (coupon is null)
            return new Error("Coupon.NotFound", "هیچ دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return coupon;
    }
}