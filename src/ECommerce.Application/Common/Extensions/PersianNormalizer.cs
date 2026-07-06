namespace ECommerce.Application.Common.Extensions;

/// <summary>
/// Provides extension methods for normalizing Persian (Farsi) text strings.
/// </summary>
/// <remarks>
/// This static class is designed to standardize Persian text by replacing common
/// Arabic-script variations with their standard Persian equivalents.
/// Common use cases include search normalization, data consistency, and user input sanitization.
/// </remarks>
public static class PersianNormalizer
{
    /// <summary>
    /// Normalizes a Persian text string by standardizing characters and formatting.
    /// </summary>
    /// <param name="text">
    /// The input Persian text string to normalize.
    /// <para>Example: " سلام عليكم " (with Arabic characters)</para>
    /// </param>
    /// <returns>
    /// A normalized Persian string with standardized characters and trimmed whitespace.
    /// <para>Returns <see cref="string.Empty"/> if the input is null or empty.</para>
    /// <para>Example: "سلام علیکم" (with standard Persian characters)</para>
    /// </returns>
    public static string NormalizePersian(this string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text.Trim()
                   .Replace("ي", "ی")
                   .Replace("ك", "ک")
                   .ToLower();
    }
}