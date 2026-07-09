using ECommerce.Application.Dtos.Payments;
using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Payments.Commands.VerifyPayment;

public class VerifyPaymentCommandHandler(IPaymentService paymentService, IUnitOfWork unitOfWork) : IRequestHandler<VerifyPaymentCommand, Result<VerifyPaymentResponseDto>>
{
    public async Task<Result<VerifyPaymentResponseDto>> Handle(VerifyPaymentCommand request, CancellationToken cancellationToken)
    {
        if (!string.Equals(request.Status, "OK", StringComparison.OrdinalIgnoreCase))
            return new Error("Payment.Cancelled", "پرداخت توسط کاربر لغو شد.", ErrorType.Canceled);

        var order = await unitOfWork.OrderRepository.GetAsync(
            expression: o => o.Id == request.OrderId,
            cancellationToken: cancellationToken);

        if (order is null)
            return new Error("Order.NotFound", "سفارش مورد نظر یافت نشد.", ErrorType.NotFound);

        var verifyResult = await paymentService.VerifyPaymentAsync(
            request.Authority,
            order.TotalAmount);

        if (verifyResult.IsFailure)
            return verifyResult.Error;

        var payment = new OrderPayment
        {
            OrderId = order.Id,
            PaymentMethod = "ZarinPal",
            TransactionId = verifyResult.Data!.RefId.ToString(),
            IsPaid = true,
            PaidAt = DateTime.UtcNow
        };

        order.Status = OrderStatus.Paid;

        await unitOfWork.OrderPaymentRepository.AddAsync(
            payment,
            cancellationToken);

        unitOfWork.OrderRepository.Update(order);

        await unitOfWork.SaveAsync(cancellationToken);

        return Result<VerifyPaymentResponseDto>.Success(
            verifyResult.Data);
    }
}