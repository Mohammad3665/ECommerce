using ECommerce.Application.Dtos.Payments;

namespace ECommerce.Application.Common.Interfaces.Services;

public interface IPaymentService
{
    Task<Result<RequestPaymentResponseDto>> RequestPaymentAsync(decimal amount, string description, string email, string mobile, long orderId);
    Task<Result<VerifyPaymentResponseDto>> VerifyPaymentAsync(string authority, decimal amount);
}