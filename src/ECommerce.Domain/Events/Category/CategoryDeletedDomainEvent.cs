using ECommerce.Domain.Common.DomainEvent;

namespace ECommerce.Domain.Events.Category;

public sealed record CategoryDeletedDomainEvent(string ImagePath) : IDomainEvent;