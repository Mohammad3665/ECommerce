using ECommerce.Domain.Events.Slide;

namespace ECommerce.Application.Features.Slides.EventHandlers;

public sealed class SlideEditedDomainEventHandler(IFileService fileService) : INotificationHandler<SlideEditedDomainEvent>
{
    public Task Handle(SlideEditedDomainEvent notification, CancellationToken cancellationToken)
    {
        fileService.DeleteFile(notification.ImagePath);
        return Task.CompletedTask;
    }
}