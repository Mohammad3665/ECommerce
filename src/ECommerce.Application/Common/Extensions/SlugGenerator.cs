using System.Text.RegularExpressions;

namespace ECommerce.Application.Common.Extensions;

/// <summary>
/// Provides extension methods for generating URL-friendly slugs from text strings.
/// </summary>
/// <remarks>
/// This static class is designed to convert various text inputs into SEO-friendly URL slugs.
/// Common use cases include product URLs, blog post slugs, category identifiers, and SEO optimization.
/// The generated slugs are safe for use in URLs and follow standard web conventions.
/// </remarks>
public static class SlugGenerator
{
    /// <summary>
    /// Converts a text string into a URL-friendly slug.
    /// </summary>
    /// <param name="value">
    /// The input text string to convert into a slug.
    /// <para>Example: "Samsung Galaxy S24 Ultra 5G"</para>
    /// </param>
    /// <returns>
    /// A URL-friendly slug string containing only lowercase letters, numbers, and hyphens.
    /// <para>Returns <see cref="string.Empty"/> if the input is null or empty.</para>
    /// <para>Example: "samsung-galaxy-s24-ultra-5g"</para>
    /// </returns>
    public static string ToSlug(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        string slug = value.ToLowerInvariant();
        slug = slug.Replace("&", "and").Replace("+", "plus");

        slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = Regex.Replace(slug, @"\s+", "-");
        slug = Regex.Replace(slug, @"-+", "-");
        slug.Trim("-");

        return slug;
    }
}
