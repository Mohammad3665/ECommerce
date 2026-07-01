namespace ECommerce.Application.Common.Extensions;

public static class PersianNormalizer
{
    public static string NormalizePersian(this string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return text.Trim()
                   .Replace("ي", "ی")
                   .Replace("ك", "ک")
                   .ToLower();
    }
}
