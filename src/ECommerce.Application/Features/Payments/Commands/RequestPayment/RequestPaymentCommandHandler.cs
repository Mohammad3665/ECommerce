using ECommerce.Application.Dtos.Payments;

namespace ECommerce.Application.Features.Payments.Commands.RequestPayment;

public class RequestPaymentCommandHandler(IPaymentService paymentService, IUnitOfWork unitOfWork) : IRequestHandler<RequestPaymentCommand, Result<RequestPaymentResponseDto>>
{
    public async Task<Result<RequestPaymentResponseDto>> Handle(RequestPaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.OrderRepository.GetAsync(
            expression: o => o.Id == request.OrderId,
            cancellationToken: cancellationToken
        );
        if (order is null)
            return new Error("Order.NotFound", "سفارش مورد نظر یافت نشد.", ErrorType.NotFound);

        return await paymentService.RequestPaymentAsync(
            amount: order.TotalAmount,
            description: $"Payment for order {order.OrderNumber}",
            email: request.Email,
            mobile: request.Mobile,
            orderId: order.Id);
    }
}