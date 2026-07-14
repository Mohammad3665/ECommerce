using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
///     Provides specification management services for product entities.
/// </summary>
/// <remarks>
///     This service handles the synchronization of product specifications (attributes/features)
///     such as color, size, weight, dimensions, brand-specific attributes, etc.
///     It manages:
///     <list type="bullet">
///         <item><description>Adding new specifications to products</description></item>
///         <item><description>Updating existing specification values</description></item>
///         <item><description>Removing specifications that are no longer needed</description></item>
///         <item><description>Maintaining specification order for display purposes</description></item>
///     </list>
/// </remarks>
public interface IProductSpecificationService
{
    /// <summary>
    ///     Synchronizes product specifications with the provided DTOs.
    ///     Deletes removed ones, updates existing ones, and creates new ones.
    /// </summary>
    /// <param name="product">The product to synchronize specifications for.</param>
    /// <param name="specifications">The desired final state of specifications with order.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task SyncAsync(Product product, ICollection<SpecificationDto> specifications, CancellationToken cancellationToken = default);
}