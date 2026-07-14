using ECommerce.Domain.Entities.Product.Events.Brand;

namespace ECommerce.Application.Features.Brands.EventHandlers;

public sealed class BrandEditedDomainEventHandler(IFileService fileService) : INotificationHandler<BrandEditedDomainEvent>
{
    public Task Handle(BrandEditedDomainEvent notification, CancellationToken cancellationToken)
    {
        fileService.DeleteFile(notification.DeletedImage);
        return Task.CompletedTask;
    }
}