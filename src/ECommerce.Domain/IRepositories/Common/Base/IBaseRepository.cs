using System.Linq.Expressions;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Domain.IRepositories.Common.Base;

/// <summary>
/// Defines the base repository contract for entities of type <typeparamref name="TEntity"/>
/// with a primary key of type <typeparamref name="TKey"/>. Provides standard CRUD, query, and existence checks.
/// </summary>
/// <typeparam name="TKey">The type of the primary key of the entity.</typeparam>
/// <typeparam name="TEntity">The entity type. Must inherit from <see cref="BaseEntity{TKey}"/>.</typeparam>
public interface IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    /// <summary>
    /// Asynchronously retrieves all entities that satisfy the optional filter, with optional ordering and included related data.
    /// </summary>
    /// <param name="expression">Optional filter expression. If <c>null</c>, all entities are returned.</param>
    /// <param name="order">Optional ordering function.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <param name="includes">Expressions specifying related entities to eagerly include.</param>
    /// <returns>A collection of matching entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    
    /// <summary>
    /// Asynchronously retrieves all entities, projected into a custom result type, with optional filtering, ordering, and included data.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="selector">The projection expression that defines the result shape.</param>
    /// <param name="expression">Optional filter expression.</param>
    /// <param name="order">Optional ordering function.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <param name="includes">Expressions specifying related entities to include.</param>
    /// <returns>A collection of projected results.</returns>
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// Asynchronously retrieves a limited number of entities, projected into a custom result type, with optional filtering, ordering, and included data.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="take">The maximum number of entities to retrieve.</param>
    /// <param name="selector">The projection expression that defines the result shape.</param>
    /// <param name="expression">Optional filter expression.</param>
    /// <param name="order">Optional ordering function.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <param name="includes">Expressions specifying related entities to include.</param>
    /// <returns>A collection of projected results, limited to <paramref name="take"/> items.</returns>
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(
        int take,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);

    /// <summary>
    /// Asynchronously retrieves a paginated result set, projected into a custom type, with optional filtering, ordering, and included data.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="current">The 1-based page number to retrieve.</param>
    /// <param name="take">The number of items per page.</param>
    /// <param name="selector">The projection expression that defines the result shape.</param>
    /// <param name="expression">Optional filter expression.</param>
    /// <param name="order">Optional ordering function.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <param name="includes">Expressions specifying related entities to include.</param>
    /// <returns>A paginated result containing the items of the requested page and pagination metadata.</returns>
    Task<Pagination<TResult>> GetAllAsync<TResult>(
        int current,
        int take,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? expression,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    
    /// <summary>
    /// Asynchronously retrieves the first entity that matches the given filter, with optional included related data.
    /// </summary>
    /// <param name="expression">The filter expression to match a single entity.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <param name="includes">Expressions specifying related entities to include.</param>
    /// <returns>The matching entity, or <c>null</c> if no entity is found.</returns>
    public Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    
    /// <summary>
    /// Asynchronously retrieves a single entity projected into a custom type, with optional included related data.
    /// </summary>
    /// <typeparam name="TResult">The type of the projected result.</typeparam>
    /// <param name="expression">The filter expression to match a single entity.</param>
    /// <param name="selector">The projection expression that defines the result shape.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <param name="includes">Expressions specifying related entities to include.</param>
    /// <returns>The projected result, or <c>null</c> if no matching entity is found.</returns>
    public Task<TResult?> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> expression,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    
    /// <summary>
    /// Asynchronously adds a new entity.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Marks an existing entity as modified.
    /// </summary>
    /// <param name="entity">The entity with updated values.</param>   
    void Update(TEntity entity);

    /// <summary>
    /// Permanently removes the entity from the underlying data store.
    /// </summary>
    /// <param name="entity">The entity to permanently delete.</param>
    void DeletePermanently(TEntity entity);

    /// <summary>
    /// Asynchronously counts the number of entities that satisfy the optional filter.
    /// </summary>
    /// <param name="expression">Optional filter expression. If <c>null</c>, counts all entities.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <returns>The number of matching entities.</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously determines whether any entity satisfies the optional filter.
    /// </summary>
    /// <param name="expression">Optional filter expression. If <c>null</c>, checks for any existing entity.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <returns><c>true</c> if at least one matching entity exists; otherwise, <c>false</c>.</returns>
    Task<bool> IsExistAsync(Expression<Func<TEntity, bool>>? expression, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a complete paginated query result containing metadata, sorting, and filters based on a QueryRequest.
    /// </summary>
    Task<Pagination<TEntity>> GetPagedListAsync(
        QueryRequest request, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a complete paginated query result, projected into a custom type, based on a QueryRequest.
    /// </summary>
    Task<Pagination<TResult>> GetPagedListAsync<TResult>(
        QueryRequest request,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken = default);
}