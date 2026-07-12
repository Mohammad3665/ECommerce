namespace ECommerce.Application.Common.Interfaces.Services;

public interface IHtmlSanitizerService
{
    string Clean(string? input);
}