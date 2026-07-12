using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Common.Interfaces.Services;

public interface IProductImageService
{
    Task<IReadOnlyCollection<string>> UpdateImagesAsync(
        Product product,
        ICollection<ProductImageDto> requestImages,
        CancellationToken cancellationToken = default);
}