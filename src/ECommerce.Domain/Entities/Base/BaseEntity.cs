using ECommerce.Domain.Common.DomainEvent;

namespace ECommerce.Domain.Entities.Base;

/// <summary>
/// Represents the base entity model for all domain objects.
/// Contains standard auditing and identification fields.
/// </summary>
/// <typeparam name="TKey">The type of the entity's primary key (e.g. int, Guid, string).</typeparam>
public class BaseEntity<TKey> : IHasDomainEvents
{
    #region Properties

    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public TKey Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// Can be null if the entity has never been updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the entity is soft-deleted.
    /// True means the entity is logically deleted but still exists in the database.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    #endregion

    #region Domain Events

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents
        => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Add(domainEvent);

    public void RemoveDomainEvent(IDomainEvent domainEvent)
        => _domainEvents.Remove(domainEvent);

    public void ClearDomainEvents()
        => _domainEvents.Clear();

    #endregion
}