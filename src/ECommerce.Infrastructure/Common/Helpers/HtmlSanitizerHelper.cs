using Ganss.Xss;

namespace ECommerce.Infrastructure.Common.Helpers;

public static class HtmlSanitizerHelper
{
    private static readonly HtmlSanitizer _sanitizer;
    static HtmlSanitizerHelper()
    {
        _sanitizer = new HtmlSanitizer();
        _sanitizer.AllowedAttributes.Add("class");
        _sanitizer.AllowedAttributes.Add("style");
    }

    public static string Clean(string? input)
    {
        return string.IsNullOrWhiteSpace(input) ? input
            ?? string.Empty : _sanitizer.Sanitize(input);
    }
}