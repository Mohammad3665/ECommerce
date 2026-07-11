using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Payments;
using ECommerce.Domain.Common.Error;

namespace ECommerce.Infrastructure.Common.Services;

public class ZarinPalPaymentService(HttpClient httpClient, IConfiguration configuration) : IPaymentService
{
    private string MerchantId => configuration["ZarinPal:MerchantId"] ?? "00000000-0000-0000-0000-000000000000";
    private string SandboxCallbackUrl => configuration["ZarinPal:CallbackUrl"]!;
    private string RequestUrl => "https://sandbox.zarinpal.com/pg/v4/payment/request.json";
    private string VerifyUrl => "https://sandbox.zarinpal.com/pg/v4/payment/verify.json";
    private string PaymentGatewayUrl => "https://sandbox.zarinpal.com/pg/StartPay/";

    public async Task<Result<RequestPaymentResponseDto>> RequestPaymentAsync(decimal amount, string description, string email, string mobile, long orderId)
    {
        var amountInRials = (long)Math.Round(amount, MidpointRounding.AwayFromZero);

        var callbackUrl = $"{SandboxCallbackUrl}?orderId={orderId}";
        var request = new ZarinPalRequestDto
        {
            MerchantId = MerchantId,
            Amount = amountInRials,
            CallbackUrl = callbackUrl,
            Description = description,
            Email = email,
            Mobile = mobile
        };

        var response = await httpClient.PostAsJsonAsync(RequestUrl, request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return new Error("Payment.RequestFailed", "ارتباط با درگاه پرداخت برقرار نشد.", ErrorType.Unexpected);

        var result = await response.Content.ReadFromJsonAsync<ZarinPalResponseDto>();

        if (result?.Data?.Authority is null || result.Data.Authority == string.Empty)
        {
            var message = string.Join(", ", result?.Errors?.Select(x => x.Message) ?? []);
            return new Error("Payment.RequestFailed", string.IsNullOrWhiteSpace(message) ? "ایجاد درخواست پرداخت با خطا مواجه شد." : message, ErrorType.Unexpected);
        }

        return new RequestPaymentResponseDto($"{PaymentGatewayUrl}{result.Data.Authority}");
    }

    public async Task<Result<VerifyPaymentResponseDto>> VerifyPaymentAsync(string authority, decimal amount)
    {
        var amountInRials = (long)Math.Round(amount, MidpointRounding.AwayFromZero);
        var request = new ZarinPalVerifyDto
        {
            MerchantId = MerchantId,
            Amount = amountInRials,
            Authority = authority
        };

        var response = await httpClient.PostAsJsonAsync(VerifyUrl, request);
        if (!response.IsSuccessStatusCode)
            return new Error("Payment.RequestFailed", "ارتباط با درگاه پرداخت برقرار نشد.", ErrorType.Unexpected);

        var result = await response.Content.ReadFromJsonAsync<ZarinPalVerifyResponseDto>();

        if (result?.Data is null)
            return new Error("Payment.VerifyFailed", "پاسخی از درگاه پرداخت دریافت نشد.", ErrorType.Unexpected);

        if (result.Data.Code != 100 && result.Data.Code != 101)
            return new Error("Payment.VerifyFailed", GetErrorMessage(result.Data.Code), ErrorType.Unexpected);


        return new VerifyPaymentResponseDto(result.Data.RefId, result.Data.CardPan, result.Data.Fee, result.Data.FeeType);
    }

    public string GetPaymentUrl(string authority)
    {
        return $"{PaymentGatewayUrl}{authority}";
    }

    private static string GetErrorMessage(int code)
    {
        return code switch
        {
            -1 => "Invalid parameters",
            -2 => "Invalid merchant ID",
            -3 => "Invalid amount",
            -4 => "Invalid callback URL",
            -5 => "Invalid mobile number",
            -6 => "Invalid email",
            -11 => "Request not found",
            -12 => "Unable to create payment request",
            -21 => "Payment not found",
            -22 => "Invalid transaction",
            -33 => "Account blocked",
            -34 => "Account limit exceeded",
            -35 => "Invalid card number",
            -40 => "Invalid request type",
            -41 => "Invalid authority",
            -42 => "Invalid amount",
            _ => $"Error code: {code}"
        };
    }
}

#region ZarinPal DTOs

internal class ZarinPalRequestDto
{
    [JsonPropertyName("merchant_id")]
    public string MerchantId { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("callback_url")]
    public string CallbackUrl { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("mobile")]
    public string? Mobile { get; set; }
}

internal class ZarinPalResponseDto
{
    [JsonPropertyName("data")]
    public ZarinPalResponseData? Data { get; set; }

    [JsonPropertyName("errors")]
    public List<ZarinPalError>? Errors { get; set; }
}

internal class ZarinPalResponseData
{
    [JsonPropertyName("authority")]
    public string Authority { get; set; } = string.Empty;

    [JsonPropertyName("fee")]
    public int Fee { get; set; }
}

internal class ZarinPalVerifyDto
{
    [JsonPropertyName("merchant_id")]
    public string MerchantId { get; set; } = string.Empty;

    [JsonPropertyName("amount")]
    public long Amount { get; set; }

    [JsonPropertyName("authority")]
    public string Authority { get; set; } = string.Empty;
}

internal class ZarinPalVerifyResponseDto
{
    [JsonPropertyName("data")]
    public ZarinPalVerifyData? Data { get; set; }

    [JsonPropertyName("errors")]
    public List<ZarinPalError>? Errors { get; set; }
}

internal class ZarinPalVerifyData
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("ref_id")]
    public long RefId { get; set; }

    [JsonPropertyName("card_pan")]
    public string CardPan { get; set; } = string.Empty;

    [JsonPropertyName("fee_type")]
    public string FeeType { get; set; } = string.Empty;

    [JsonPropertyName("fee")]
    public int Fee { get; set; }
}

internal class ZarinPalError
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; } = string.Empty;
}

#endregion
