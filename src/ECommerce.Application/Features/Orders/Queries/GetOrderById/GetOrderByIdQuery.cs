using ECommerce.Application.Dtos.Orders;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(long OrderId) : IRequest<Result<OrderDetailResponseDto>>;
