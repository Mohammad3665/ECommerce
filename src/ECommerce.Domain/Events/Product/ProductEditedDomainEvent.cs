using ECommerce.Domain.Common.DomainEvent;

namespace ECommerce.Domain.Events.Product;

public sealed record ProductEditedDomainEvent(IReadOnlyCollection<string> DeletedImages) : IDomainEvent;