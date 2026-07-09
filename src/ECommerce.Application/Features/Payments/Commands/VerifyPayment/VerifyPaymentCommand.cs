using ECommerce.Application.Dtos.Payments;

namespace ECommerce.Application.Features.Payments.Commands.VerifyPayment;

public record VerifyPaymentCommand(
    string Authority,
    string Status,
    long OrderId
) : IRequest<Result<VerifyPaymentResponseDto>>;