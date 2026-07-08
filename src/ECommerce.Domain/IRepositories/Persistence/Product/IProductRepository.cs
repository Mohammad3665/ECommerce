using System.Linq.Expressions;

namespace ECommerce.Domain.IRepositories.Persistence.Product;

public interface IProductRepository : IBaseRepository<long, Entities.Product.Product>
{
    Task<List<Entities.Product.Product>> GetAllWithTrackingAsync(
        Expression<Func<Entities.Product.Product, bool>>? expression = null,
        Func<IQueryable<Entities.Product.Product>, IOrderedQueryable<Entities.Product.Product>>? order = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<Entities.Product.Product, object>>[] includes);
}