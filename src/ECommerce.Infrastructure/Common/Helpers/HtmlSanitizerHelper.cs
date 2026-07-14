using Ganss.Xss;

namespace ECommerce.Infrastructure.Common.Helpers;

/// <summary>
///     Provides HTML sanitization helper methods to prevent XSS (Cross-Site Scripting) attacks.
/// </summary>
/// <remarks>
///     <para>
///         This helper wraps the Ganss.Xss <see cref="HtmlSanitizer"/> library with
///         application-specific configuration and a simplified API.
///     </para>
/// </remarks>
public static class HtmlSanitizerHelper
{
    private static readonly HtmlSanitizer _sanitizer;
    static HtmlSanitizerHelper()
    {
        _sanitizer = new HtmlSanitizer();
        _sanitizer.AllowedAttributes.Add("class");
        _sanitizer.AllowedAttributes.Add("style");
    }

    /// <summary>
    ///     Sanitizes HTML content by removing dangerous tags and attributes.
    /// </summary>
    /// <param name="input">
    ///     The HTML string to sanitize. Can be <c>null</c>, empty, or contain only whitespace.
    /// </param>
    /// <returns>
    ///     A sanitized HTML string containing only allowed tags and attributes.
    ///     <list type="bullet">
    ///         <item><description>If <paramref name="input"/> is <c>null</c>, returns <see cref="string.Empty"/></description></item>
    ///         <item><description>If <paramref name="input"/> is whitespace, returns <see cref="string.Empty"/></description></item>
    ///         <item><description>If <paramref name="input"/> contains only dangerous content, returns <see cref="string.Empty"/></description></item>
    ///         <item><description>Otherwise, returns the sanitized HTML string</description></item>
    ///     </list>
    /// </returns>
    public static string Clean(string? input)
    {
        return string.IsNullOrWhiteSpace(input) ? input
            ?? string.Empty : _sanitizer.Sanitize(input);
    }
}