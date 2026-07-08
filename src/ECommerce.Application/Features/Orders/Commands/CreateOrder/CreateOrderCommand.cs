using ECommerce.Application.Dtos.Orders;
using ECommerce.Domain.Entities.Order;

namespace ECommerce.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    string FullName,
    string PhoneNumber,
    string Address,
    string PostalCode,
    string? CouponCode
) : IRequest<Result<CreateOrderResponseDto>>;