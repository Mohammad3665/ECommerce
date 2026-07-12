using ECommerce.Domain.Common.DomainEvent;

namespace ECommerce.Domain.Events.Slide;

public sealed record SlideDeletedDomainEvent(string ImagePath) : IDomainEvent;
