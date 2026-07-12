using ECommerce.Application.Common.Extensions;
using ECommerce.Application.Common.Interfaces.Services;

namespace ECommerce.Infrastructure.Common.Services;

public class SlugService(IUnitOfWork unitOfWork) : ISlugService
{
    public async Task<string> GenerateProductSlugAsync(string englishName, long? productId = null, CancellationToken cancellationToken = default)
    {
        var baseSlug = englishName.ToSlug();

        var slug = baseSlug;

        var index = 1;
        while (await unitOfWork.ProductRepository.IsExistAsync(p => p.Slug == slug && (!productId.HasValue || p.Id != productId.Value), cancellationToken))
        {
            slug = $"{baseSlug}-{index++}";
        }
        return slug;
    }
}