namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
///     Provides HTML sanitization functionality to prevent XSS (Cross-Site Scripting) attacks.
/// </summary>
/// <remarks>
///     This service removes or encodes potentially dangerous HTML tags and attributes
///     from user-generated content such as comments, article bodies, and product descriptions.
/// </remarks>
public interface IHtmlSanitizerService
{
    /// <summary>
    ///     Sanitizes the specified HTML input by removing unsafe tags and attributes.
    /// </summary>
    /// <param name="input">
    ///     The HTML string to be sanitized. Can be <c>null</c> or empty.
    /// </param>
    /// <returns>
    ///     A sanitized HTML string with all dangerous content removed.
    ///     Returns <c>string.Empty</c> if the input is <c>null</c>, empty, or contains only unsafe content.
    /// </returns>
    string Clean(string? input);
}