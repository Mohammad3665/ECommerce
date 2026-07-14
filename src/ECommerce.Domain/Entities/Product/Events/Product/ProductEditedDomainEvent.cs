using ECommerce.Domain.Abstractions.DomainEvent;

namespace ECommerce.Domain.Entities.Product.Events.Product;

public sealed record ProductEditedDomainEvent(IReadOnlyCollection<string> DeletedImages) : IDomainEvent;