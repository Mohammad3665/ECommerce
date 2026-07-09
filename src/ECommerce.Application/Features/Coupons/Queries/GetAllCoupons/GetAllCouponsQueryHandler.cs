using ECommerce.Application.Dtos.Coupons;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Coupons.Queries.GetAllCoupons;

public class GetAllCouponsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCouponsQuery, Result<Pagination<GetAllCouponsResponseDto>>>
{
    public async Task<Result<Pagination<GetAllCouponsResponseDto>>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
    {
        var coupons = await unitOfWork.CouponRepository.GetPagedListAsync<GetAllCouponsResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );
        if (coupons is null)
            return new Error("Coupon.NotFound", "هیچ دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return coupons;
    }
}
