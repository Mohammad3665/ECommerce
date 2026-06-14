using ECommerce.Domain.IRepositories.Persistence.Product;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class ProductRepository(ApplicationDbContext context) 
    : BaseRepository<long, Domain.Entities.Product.Product>(context), IProductRepository;