using ECommerce.Domain.Entities.Product.Events.Category;

namespace ECommerce.Application.Features.Categories.EventHandlers;

public sealed class CategoryDeletedDomainEventHandler(IFileService fileService) : INotificationHandler<CategoryDeletedDomainEvent>
{
    public Task Handle(CategoryDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        fileService.DeleteFile(notification.ImagePath);
        return Task.CompletedTask;
    }
}