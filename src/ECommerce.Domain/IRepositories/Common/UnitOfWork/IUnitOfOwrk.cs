using ECommerce.Domain.Common.Result;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Domain.IRepositories.Common.UnitOfWork;

/// <summary>
/// This is like a huge boss that not only controls the treasure chests but also handles magic spells (transactions) 
/// to make sure all changes happen together or not at all, like casting a spell that either works completely or gets undone.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
    /// <summary>
    /// Saves all the changes without a magic spell (transaction).
    /// </summary>
    /// <returns>Success or failure result.</returns>
    Task<Result> SaveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts a magic spell (transaction) to group changes.
    /// </summary>
    /// <returns>A special handler for the spell.</returns>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finishes the spell successfully (commit), so all changes stay.
    /// </summary>
    /// <param name="transaction">The spell handler.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Success or failure.</returns>
    Task<Result> CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);

    /// <summary>
    /// Undoes the spell (rollback) if something goes wrong, so no changes happen.
    /// </summary>
    /// <param name="transaction">The spell handler.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Success or failure.</returns>
    Task<Result> RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default);
}