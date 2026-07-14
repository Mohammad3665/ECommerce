using ECommerce.Domain.Entities.Product.Events.Product;

namespace ECommerce.Application.Features.Products.EventHandlers;

public sealed class ProductDeletedDomainEventHandler(IFileService fileService) : INotificationHandler<ProductDeletedDomainEvent>
{
    public async Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        await fileService.DeleteFilesAsync(notification.ImagePaths);
    }
}