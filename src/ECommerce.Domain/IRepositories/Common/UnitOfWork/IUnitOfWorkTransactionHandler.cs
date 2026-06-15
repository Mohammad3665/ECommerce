using ECommerce.Domain.Common.Result;

namespace ECommerce.Domain.IRepositories.Common.UnitOfWork;

public interface IUnitOfWorkTransactionHandler : IAsyncDisposable
{
    /// <summary>
    /// Executes multiple operations that return <see cref="Result"/>.
    /// </summary>
    Task<Result> ExecuteAsync(Func<IUnitOfWork, CancellationToken, Task<Result>> action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes multiple operations that return <see cref="Result{T}"/>.
    /// </summary>
    Task<Result<T>> ExecuteAsync<T>(Func<IUnitOfWork, CancellationToken, Task<Result<T>>> action, CancellationToken cancellationToken = default);
}