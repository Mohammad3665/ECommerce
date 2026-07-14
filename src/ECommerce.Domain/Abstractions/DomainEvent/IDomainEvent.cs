using MediatR;

namespace ECommerce.Domain.Abstractions.DomainEvent;

/// <summary>
///     Represents a domain event that occurs within the domain layer.
/// </summary>
/// <remarks>
///     <para>
///         Domain events are used to capture significant business occurrences
///         within the domain model. They enable:
///     </para>
/// </remarks>
public interface IDomainEvent : INotification
{

}