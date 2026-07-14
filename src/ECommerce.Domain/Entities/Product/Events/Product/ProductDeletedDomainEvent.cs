using ECommerce.Domain.Abstractions.DomainEvent;

namespace ECommerce.Domain.Entities.Product.Events.Product;

public sealed record ProductDeletedDomainEvent(IReadOnlyList<string> ImagePaths) : IDomainEvent;