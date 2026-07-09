namespace ECommerce.Application.Dtos.Payments;

public record VerifyPaymentResponseDto(
    long RefId,
    string CardPan,
    int Fee,
    string FeeType
);