namespace ECommerce.Domain.IRepositories.Common.UnitOfWork;

/// <summary>
/// Handles transactional operations across multiple repositories using Unit of Work pattern.
/// </summary>
/// <remarks>
/// Ensures data consistency by executing operations within a database transaction.
/// Automatically commits on success or rolls back on failure.
/// Implements IAsyncDisposable for proper resource cleanup.
/// </remarks>
public interface IUnitOfWorkTransactionHandler : IAsyncDisposable
{
    /// <summary>
    /// Executes multiple operations within a transaction that return a <see cref="Result"/>.
    /// </summary>
    /// <param name="action">The async function containing repository operations to execute.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A Result indicating success or failure of the transaction.</returns>
    /// <remarks>
    /// <para>Usage example:</para>
    /// <code>
    /// var result = await handler.ExecuteAsync(async (uow, ct) => {
    ///     await uow.Products.AddAsync(newProduct, ct);
    ///     await uow.Inventories.UpdateStockAsync(productId, quantity, ct);
    ///     return Result.Success();
    /// });
    /// </code>
    /// </remarks>
    Task<Result> ExecuteAsync(Func<IUnitOfWork, CancellationToken, Task<Result>> action, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes multiple operations within a transaction that return a <see cref="Result{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of data returned on success.</typeparam>
    /// <param name="action">The async function containing repository operations to execute.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A Result containing the data on success, or error on failure.</returns>
    /// <remarks>
    /// <para>Usage example:</para>
    /// <code>
    /// var result = await handler.ExecuteAsync(async (uow, ct) => {
    ///     var order = await uow.Orders.AddAsync(newOrder, ct);
    ///     await uow.Payments.ProcessAsync(order.Id, ct);
    ///     return Result&lt;OrderDto&gt;.Success(order.Adapt&lt;OrderDto&gt;());
    /// });
    /// </code>
    /// </remarks>
    Task<Result<T>> ExecuteAsync<T>(Func<IUnitOfWork, CancellationToken, Task<Result<T>>> action, CancellationToken cancellationToken = default);
}