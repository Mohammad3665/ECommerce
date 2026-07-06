namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
/// Defines a service for generating secure email verification codes.
/// </summary>
/// <remarks>
/// This interface is designed for generating one-time security codes that are sent to users via email.
/// Implementations must ensure cryptographic randomness and code expiration handling.
/// Common use cases include account confirmation, password reset, and two-factor authentication.
/// </remarks>
public interface ICodeGeneratorService
{
    /// <summary>
    /// Generates a secure alphanumeric verification code for email-based authentication.
    /// </summary>
    /// <returns>
    /// A cryptographically secure string containing the generated verification code.
    /// </returns>
    string Generate();
}