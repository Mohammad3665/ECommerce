using ECommerce.Application.Dtos.Coupons;

namespace ECommerce.Application.Features.Coupons.Queries.GetCouponById;

public record GetCouponByIdQuery(Guid Id) : IRequest<Result<GetCouponResponseDto>>;
