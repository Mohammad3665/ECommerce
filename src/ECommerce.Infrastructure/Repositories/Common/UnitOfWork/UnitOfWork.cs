using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Infrastructure.Repositories.Common.UnitOfWork;

/// <summary>
/// The real super boss that implements the IUnitOfWork blueprint, handling repositories and magic spells (transactions).
/// </summary>
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    #region Repositories

    #endregion

    #region Save

    public async Task<Result> SaveAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (DbUpdateException ex)
        {
            var error = new Error(
                "Database.SaveError",
                $"Failed to save changes to database.{ex.Message}",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }
        catch (OperationCanceledException)
        {
            var error = new Error(
                "Database.OperationCanceled",
                $"Operation canceled.",
                ErrorType.Canceled
            );
            return Result.Failure(error);
        }
        catch (Exception ex)
        {
            var error = new Error(
                "Database.UnexpectedError",
                $"An unexpected error occurred: {ex.Message}",
                ErrorType.Unexpected
            );

            return Result.Failure(error);
        }
    }

    #endregion

    #region Transaction

    Task<Result> IUnitOfWork.SaveAsync(CancellationToken cancellationToken)
    {
        return SaveAsync(cancellationToken);
    }
    Task<IDbContextTransaction> IUnitOfWork.BeginTransactionAsync(CancellationToken cancellationToken)
    {
        return BeginTransactionAsync(cancellationToken);
    }

    Task<Result> IUnitOfWork.CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        return CommitTransactionAsync(transaction, cancellationToken);
    }

    Task<Result> IUnitOfWork.RollbackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        return RollbackTransactionAsync(transaction, cancellationToken);
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task<Result> CommitTransactionAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken = default)
    {
        if (transaction is null)
        {
            var error = new Error(
                "Transaction.Null",
                "Transaction cannot be null.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        try
        {
            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            var error = new Error(
                "Database.OperationCanceled",
                $"Operation canceled.",
                ErrorType.Canceled
            );
            return Result.Failure(error);
        }
        catch (Exception ex)
        {
            var error = new Error(
                "Database.UnexpectedError",
                $"An unexpected error occurred: {ex.Message}",
                ErrorType.Unexpected
            );

            return Result.Failure(error);
        }
    }

    public async Task<Result> RollbackTransactionAsync(IDbContextTransaction? transaction, CancellationToken cancellationToken = default)
    {
        if (transaction is null)
        {
            var error = new Error(
                "Transaction.Null",
                "Transaction cannot be null.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        try
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            var error = new Error(
                "Database.OperationCanceled",
                $"Rollback canceled.",
                ErrorType.Canceled
            );
            return Result.Failure(error);
        }
        catch (Exception ex)
        {
            var error = new Error(
                "Database.UnexpectedError",
                $"An unexpected error occurred: {ex.Message}",
                ErrorType.Unexpected
            );

            return Result.Failure(error);
        }

    }

    #endregion

    #region Dispose

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }

    #endregion
}
