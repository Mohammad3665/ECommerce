using ECommerce.Domain.Abstractions.DomainEvent;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ECommerce.Infrastructure.Persistence.Interceptors;

/// <summary>
///     Entity Framework Core interceptor that automatically dispatches domain events after SaveChanges.
/// </summary>
/// <remarks>
///     <para>
///         This interceptor captures domain events from entities that implement <see cref="IHasDomainEvents"/>
///         and dispatches them using MediatR after the database transaction has been successfully committed.
///     </para>
/// </remarks>
public sealed class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return result;

        var domainEntities = eventData.Context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        foreach (var entity in domainEntities)
            entity.ClearDomainEvents();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent, cancellationToken);

        return result;
    }
}