using System.Linq.Expressions;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Base;
using ECommerce.Domain.IRepositories.Common.Base;
using ECommerce.Domain.Specifications.Common;
using ECommerce.Infrastructure.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.Common.Base;

public class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<TEntity> Db;
    protected BaseRepository(ApplicationDbContext context)
    {
        Context = context;
        Db = context.Set<TEntity>();
    }

    protected virtual IQueryable<TEntity> BuildQuery(
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
        bool asNoTracking = true,
        bool asSplitQuery = false,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = Db;

        if (expression is not null)
            query = query.Where(expression);

        if (includes.Length > 0)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (order is not null)
            query = order(query);
        
        if (asNoTracking)
            query = query.AsNoTracking();
        
        if (asSplitQuery)
            query = query.AsSplitQuery();
        
        return query;
    }


    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await Db.AddAsync(entity, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        Db.Update(entity);
    }

    public virtual void DeletePermanently(TEntity entity)
    {
        Db.Remove(entity);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? expression = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null, 
        CancellationToken cancellationToken = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, order, true, false, includes);
        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? expression = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null, 
        CancellationToken cancellationToken = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, order, true, false, includes);
        return await query.Select(selector).ToListAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(
        int take, 
        Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? expression, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null, 
        CancellationToken cancellationToken = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, order, true, false, includes);

        take = take <= 0 ? 10 : take;
        return await query.Select(selector).Take(take).ToListAsync();
    }

    public virtual async Task<Pagination<TResult>> GetAllAsync<TResult>(
        int current, 
        int take, 
        Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? expression, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null, 
        CancellationToken cancellationToken = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, order, true, false, includes);

        take = take <= 0 ? 10 : take;
        current = current <= 0 ? 1 : current;

        int count = await query.CountAsync(cancellationToken: cancellationToken);
        if (count == 0)
            return new Pagination<TResult>([], current, 0, take);
        
        int skip = (current - 1) * take;
        if (skip <= count)
        {
            current = (int)Math.Ceiling(count / (decimal)take);
            current = current <= 0 ? 1 : current;
            skip = (current -1 ) * take;
        }

        var items = await query.Select(selector).Skip(skip).Take(take).ToListAsync(cancellationToken: cancellationToken);
        return new Pagination<TResult>(items, current, count, take);
    }

    public virtual async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> expression, 
        CancellationToken cancellationToken = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, null, true, false, includes);
        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<TResult?> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> expression, 
        Expression<Func<TEntity, TResult>> selector, 
        CancellationToken cancellationToken = default, 
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(expression, null, true, false, includes);
        return await query.Select(selector).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<bool> IsExistAsync(
        Expression<Func<TEntity, bool>>? expression, 
        CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(expression);
        return await query.AnyAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? expression = null, 
        CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(expression);
        return await query.CountAsync(cancellationToken: cancellationToken);
    }

    public virtual async Task<Pagination<TEntity>> GetPagedListAsync(
        QueryRequest request, 
        CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(null, null, true, false);

        query = query.ApplyFilters(request.Filters);
        query = query.ApplySorting(request.SortBy, request.Sort);

        int totalCount = await query.CountAsync(cancellationToken: cancellationToken);

        var page = request.Page <= 0 ? 1 : request.Page;
        var take = request.Take <= 0 ? 10 : request.Take;
        var skip = (page - 1) * take;

        var items = await query.Skip(skip).Take(take).ToListAsync(cancellationToken: cancellationToken);
        return new Pagination<TEntity>(items, page, totalCount, take);
    }

    public virtual async Task<Pagination<TResult>> GetPagedListAsync<TResult>(QueryRequest request, Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(null, null, true, false);

        query = query.ApplyFilters(request.Filters);
        query = query.ApplySorting(request.SortBy, request.Sort);

        int totalCount = await query.CountAsync(cancellationToken: cancellationToken);

        var page = request.Page <= 0 ? 1 : request.Page;
        var take = request.Take <= 0 ? 10 : request.Take;
        var skip = (page - 1) * take;

        var items = await query.Select(selector).Skip(skip).Take(take).ToListAsync(cancellationToken: cancellationToken);
        return new Pagination<TResult>(items, page, totalCount, take);
    }
    
}