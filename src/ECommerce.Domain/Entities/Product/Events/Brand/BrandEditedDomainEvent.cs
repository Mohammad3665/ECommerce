using ECommerce.Domain.Abstractions.DomainEvent;

namespace ECommerce.Domain.Entities.Product.Events.Brand;

public sealed record BrandEditedDomainEvent(string DeletedImage) : IDomainEvent;