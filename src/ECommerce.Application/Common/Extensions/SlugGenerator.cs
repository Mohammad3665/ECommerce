using System.Text.RegularExpressions;

namespace ECommerce.Application.Common.Extensions;

public static class SlugGenerator
{
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
