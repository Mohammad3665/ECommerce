using ECommerce.Application.Dtos.Payments;

namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
///     Provides payment processing services integrating with external payment gateways.
/// </summary>
/// <remarks>
///     <para>
///         This service handles the complete payment lifecycle including:
///     </para>
///     <list type="bullet">
///         <item><description><b>Request Payment:</b> Initiates a payment request with the gateway</description></item>
///         <item><description><b>Verify Payment:</b> Confirms and validates the payment after callback from gateway</description></item>
///     </list>
///     <para>
///         <b>Supported Gateways:</b> Zarinpal, Mellat, Saman, etc. (configurable via settings)
///     </para>
///     <para>
///         <b>Flow:</b>
///         <list type="number">
///             <item><description>Client calls <see cref="RequestPaymentAsync"/> → Returns payment URL</description></item>
///             <item><description>User is redirected to payment gateway</description></item>
///             <item><description>After payment, gateway redirects back with an authority/token</description></item>
///             <item><description>Client calls <see cref="VerifyPaymentAsync"/> with the authority to confirm payment</description></item>
///         </list>
///     </para>
/// </remarks>
public interface IPaymentService
{
    /// <summary>
    ///     Initiates a payment request with the configured payment gateway.
    /// </summary>
    /// <param name="amount">
    ///     The payment amount in Rial (Iranian currency) or the gateway's currency.
    ///     Must be greater than zero.
    /// </param>
    /// <param name="description">
    ///     A description of the payment (e.g., "Order #12345 - Payment").
    ///     This will be displayed to the user on the payment page.
    /// </param>
    /// <param name="email">
    ///     The customer's email address for payment confirmation and notifications.
    ///     Must be a valid email format.
    /// </param>
    /// <param name="mobile">
    ///     The customer's mobile phone number for payment notifications.
    ///     Must be a valid mobile number format.
    /// </param>
    /// <param name="orderId">
    ///     The unique identifier of the order being paid for.
    ///     This is used to link the payment to the order in the system.
    /// </param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the payment request response:
    ///     <list type="bullet">
    ///         <item><description><b>On Success:</b> Contains <see cref="RequestPaymentResponseDto"/> with the payment URL (redirect URL) and authority code</description></item>
    ///         <item><description><b>On Failure:</b> Contains error details explaining why the payment request failed</description></item>
    ///     </list>
    /// </returns>
    Task<Result<RequestPaymentResponseDto>> RequestPaymentAsync(decimal amount, string description, string email, string mobile, long orderId);

    /// <summary>
    ///     Verifies and validates a payment transaction after the user returns from the payment gateway.
    /// </summary>
    /// <param name="authority">
    ///     The authority code or token returned by the payment gateway after payment.
    ///     This is used to identify the transaction for verification.
    /// </param>
    /// <param name="amount">
    ///     The original payment amount that was requested.
    ///     Must match the amount the gateway verifies to prevent tampering.
    /// </param>
    /// <returns>
    ///     A <see cref="Result{T}"/> containing the verification response:
    ///     <list type="bullet">
    ///         <item><description><b>On Success:</b> Contains <see cref="VerifyPaymentResponseDto"/> with:
    ///             <list type="bullet">
    ///                 <item><description>Payment status (success/failure)</description></item>
    ///                 <item><description>Transaction reference number (refId) from the gateway</description></item>
    ///                 <item><description>Verified amount</description></item>
    ///                 <item><description>Additional gateway-specific details</description></item>
    ///             </list>
    ///         </description></item>
    ///         <item><description><b>On Failure:</b> Contains error details explaining why verification failed</description></item>
    ///     </list>
    /// </returns>
    Task<Result<VerifyPaymentResponseDto>> VerifyPaymentAsync(string authority, decimal amount);
}