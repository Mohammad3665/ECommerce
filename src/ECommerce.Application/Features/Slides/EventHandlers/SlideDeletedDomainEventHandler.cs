using ECommerce.Domain.Events.Slide;

namespace ECommerce.Application.Features.Slides.EventHandlers;

public sealed class SlideDeletedDomainEventHandler(IFileService fileService) : INotificationHandler<SlideDeletedDomainEvent>
{
    public Task Handle(SlideDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        fileService.DeleteFile(notification.ImagePath);
        return Task.CompletedTask;
    }
}