using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
///     Service for managing product images (upload, delete, and order).
/// </summary>
public interface IProductImageService
{
    /// <summary>
    ///     Synchronizes product images with the provided DTOs.
    ///     Uploads new images, deletes removed ones, and updates ordering.
    /// </summary>
    /// <param name="product">The product to update images for.</param>
    /// <param name="requestImages">Desired final state of images (with order).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Collection of image URLs in the final order.</returns>
    Task<IReadOnlyCollection<string>> UpdateImagesAsync(Product product, ICollection<ProductImageDto> requestImages, CancellationToken cancellationToken = default);
}