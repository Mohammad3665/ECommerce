using ECommerce.Domain.Abstractions.DomainEvent;

namespace ECommerce.Domain.Entities.Product.Events.Category;

public sealed record CategoryEditedDomainEvent(string DeletedImage) : IDomainEvent;