using ECommerce.Application.Dtos.Orders;
using Mapster;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<GetOrderByIdQuery, Result<OrderDetailResponseDto>>
{
    public async Task<Result<OrderDetailResponseDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        var userId = currentUser.UserId.Value;
        var order = await unitOfWork.OrderRepository.GetAsync(
            expression: o => o.Id == request.OrderId,
            cancellationToken: cancellationToken,
            includes: [
                o => o.Items,
                o => o.Histories
            ]
        );
        if (order is null)
            return new Error("Order.NotFound", "سفارش یافت نشد.", ErrorType.NotFound);

        if (order.UserId != userId)
            return new Error("Order.NotFound", "سفارش یافت نشد.", ErrorType.NotFound);

        var shipping = await unitOfWork.OrderShippingRepository.GetAsync<OrderShippingDto>(
            expression: os => os.OrderId == order.Id,
            cancellationToken: cancellationToken
        );
        if (shipping is null)
            return new Error("Order.ShippingNotFound", "اطلاعات حمل و نقل برای این سفارش یافت نشد.", ErrorType.NotFound);

        var orderDetail = new OrderDetailResponseDto(
            OrderId: order.Id,
            OrderNumber: order.OrderNumber,
            Status: order.Status,
            OrderDate: order.OrderDate,
            SubTotal: order.SubTotal,
            DiscountAmount: order.DiscountAmount,
            ShippingCost: order.ShippingCost,
            TotalAmount: order.TotalAmount,
            Items: order.Items.Adapt<List<OrderItemDto>>(),
            Shipping: shipping,
            History: order.Histories.Adapt<List<OrderHistoryDto>>()
        );

        return orderDetail;
    }
}