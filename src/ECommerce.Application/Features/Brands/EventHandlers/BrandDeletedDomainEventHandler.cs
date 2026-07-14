using ECommerce.Domain.Entities.Product.Events.Brand;

namespace ECommerce.Application.Features.Brands.EventHandlers;

public sealed class BrandDeletedDomainEventHandler(IFileService fileService) : INotificationHandler<BrandDeletedDomainEvent>
{
    public Task Handle(BrandDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        fileService.DeleteFile(notification.ImagePath);
        return Task.CompletedTask;
    }
}