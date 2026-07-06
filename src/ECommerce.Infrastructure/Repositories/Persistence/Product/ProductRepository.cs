using ECommerce.Domain.IRepositories.Persistence.Product;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class ProductRepository(ApplicationDbContext context) 
    : BaseRepository<long, Domain.Entities.Product.Product>(context), IProductRepository;