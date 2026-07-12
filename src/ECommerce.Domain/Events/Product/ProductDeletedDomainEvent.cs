using ECommerce.Domain.Common.DomainEvent;

namespace ECommerce.Domain.Events.Product;

public sealed record ProductDeletedDomainEvent(IReadOnlyList<string> ImagePaths) : IDomainEvent;