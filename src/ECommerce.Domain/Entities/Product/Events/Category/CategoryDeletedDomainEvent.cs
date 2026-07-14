using ECommerce.Domain.Abstractions.DomainEvent;

namespace ECommerce.Domain.Entities.Product.Events.Category;

public sealed record CategoryDeletedDomainEvent(string ImagePath) : IDomainEvent;