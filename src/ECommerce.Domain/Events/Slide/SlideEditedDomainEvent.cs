using ECommerce.Domain.Common.DomainEvent;

namespace ECommerce.Domain.Events.Slide;

public sealed record SlideEditedDomainEvent(string ImagePath) : IDomainEvent;