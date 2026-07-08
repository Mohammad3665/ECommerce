using ECommerce.Application.Dtos.Orders;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderHistory;

public class GetOrderHistoryQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<GetOrderHistoryQuery, Result<IEnumerable<OrderHistoryResponseDto>>>
{
    public async Task<Result<IEnumerable<OrderHistoryResponseDto>>> Handle(GetOrderHistoryQuery request, CancellationToken cancellationToken)
    {
        if (currentUser.UserId is null)
            return new Error("Auth.Unauthorized", "کاربر احراز هویت نشده است.", ErrorType.Unauthorized);

        var userId = currentUser.UserId.Value;

        var orders = await unitOfWork.OrderRepository.GetAllAsync(
            expression: o => o.UserId == userId,
            order: o => o.OrderByDescending(x => x.OrderDate),
            cancellationToken: cancellationToken,
            includes: o => o.Items
        );

        var result = orders.Select(o => new OrderHistoryResponseDto(
            o.Id,
            o.OrderNumber,
            o.Status,
            o.TotalAmount,
            o.OrderDate,
            o.Items.Count
        )).ToList();

        return Result<IEnumerable<OrderHistoryResponseDto>>.Success(result);
    }
}
