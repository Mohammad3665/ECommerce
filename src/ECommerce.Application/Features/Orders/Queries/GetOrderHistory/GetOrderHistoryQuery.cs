using ECommerce.Application.Dtos.Orders;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderHistory;

public record GetOrderHistoryQuery : IRequest<Result<IEnumerable<OrderHistoryResponseDto>>>;
