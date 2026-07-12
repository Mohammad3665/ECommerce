using ECommerce.Domain.Events.Product;

namespace ECommerce.Application.Features.Products.EventHandlers;

public sealed class ProductEdittedDomainEventHandler(IFileService fileService) : INotificationHandler<ProductEditedDomainEvent>
{
    public async Task Handle(ProductEditedDomainEvent notification, CancellationToken cancellationToken)
    {
        await fileService.DeleteFilesAsync(notification.DeletedImages, cancellationToken);
    }
}