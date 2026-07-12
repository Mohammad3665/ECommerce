using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Infrastructure.Common.Helpers;

namespace ECommerce.Infrastructure.Common.Services;

public class HtmlSanitizerService : IHtmlSanitizerService
{
    public string Clean(string? input)
    {
        return HtmlSanitizerHelper.Clean(input);
    }
}