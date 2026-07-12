using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Common.Interfaces.Services;

public interface IProductSpecificationService
{
    Task SyncAsync(
        Product product,
        ICollection<SpecificationDto> specifications,
        CancellationToken cancellationToken = default);
}