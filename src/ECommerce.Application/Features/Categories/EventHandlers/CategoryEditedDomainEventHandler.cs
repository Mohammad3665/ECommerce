using ECommerce.Domain.Events.Category;

namespace ECommerce.Application.Features.Categories.EventHandlers;

public sealed class CategoryEditedDomainEventHandler(IFileService fileService) : INotificationHandler<CategoryEditedDomainEvent>
{
    public Task Handle(CategoryEditedDomainEvent notification, CancellationToken cancellationToken)
    {
        fileService.DeleteFile(notification.DeletedImage);
        return Task.CompletedTask;
    }
}