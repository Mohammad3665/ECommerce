using System.Linq.Expressions;
using ECommerce.Domain.IRepositories.Persistence.Product;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class ProductRepository(ApplicationDbContext context)
    : BaseRepository<long, Domain.Entities.Product.Product>(context), IProductRepository
{
    public async Task<List<Domain.Entities.Product.Product>> GetAllWithTrackingAsync(
        Expression<Func<Domain.Entities.Product.Product, bool>>? expression = null,
        Func<IQueryable<Domain.Entities.Product.Product>,
        IOrderedQueryable<Domain.Entities.Product.Product>>? order = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<Domain.Entities.Product.Product, object>>[] includes)
    {
        var query = BuildQuery(expression, order, false, false, includes);
        return await query.ToListAsync(cancellationToken: cancellationToken);
    }
}