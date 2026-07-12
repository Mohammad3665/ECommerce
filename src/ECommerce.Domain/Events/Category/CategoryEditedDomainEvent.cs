using ECommerce.Domain.Common.DomainEvent;

namespace ECommerce.Domain.Events.Category;

public sealed record CategoryEditedDomainEvent(string DeletedImage) : IDomainEvent;