using ECommerce.Application.Dtos.Payments;

namespace ECommerce.Application.Features.Payments.Commands.RequestPayment;

public record RequestPaymentCommand(
    long OrderId,
    string Email,
    string Mobile
) : IRequest<Result<RequestPaymentResponseDto>>;
