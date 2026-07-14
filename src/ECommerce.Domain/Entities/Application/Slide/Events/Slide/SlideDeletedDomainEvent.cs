using ECommerce.Domain.Abstractions.DomainEvent;

namespace ECommerce.Domain.Entities.Slide.Events.Slide;

public sealed record SlideDeletedDomainEvent(string ImagePath) : IDomainEvent;
