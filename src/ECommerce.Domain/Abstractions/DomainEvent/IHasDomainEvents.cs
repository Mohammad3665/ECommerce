namespace ECommerce.Domain.Abstractions.DomainEvent;

/// <summary>
///     Represents an entity that can raise and manage domain events.
/// </summary>
/// <remarks>
///     <para>
///         This interface is typically implemented by aggregate roots or entities
///         that need to track domain events that occur during their lifecycle.
///         It provides a way to collect, store, and clear domain events before
///         they are dispatched to handlers.
///     </para>
/// </remarks>
public interface IHasDomainEvents
{
    /// <summary>
    ///     Gets the collection of domain events that have been raised by this entity.
    /// </summary>
    /// <value>
    ///     A read-only collection of <see cref="IDomainEvent"/> instances.
    ///     Returns an empty collection if no events have been raised.
    /// </value>
    /// <remarks>
    ///     <para>
    ///         <b>Read-Only:</b> The collection is read-only to prevent external
    ///         modifications. Events should only be added through internal methods
    ///         (e.g., <c>AddDomainEvent</c>) to ensure proper encapsulation.
    ///     </para>
    /// </remarks>
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

    /// <summary>
    ///     Removes all domain events from the entity.
    /// </summary>
    void ClearDomainEvents();
}