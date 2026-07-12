namespace ECommerce.Application.Common.Interfaces.Services;

public interface ISlugService
{
    Task<string> GenerateProductSlugAsync(
        string englishName,
        long? productId = null,
        CancellationToken cancellationToken = default);
}