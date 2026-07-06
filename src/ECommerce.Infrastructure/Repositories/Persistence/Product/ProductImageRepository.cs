using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Persistence.Product;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class ProductImageRepository(ApplicationDbContext context) 
    : BaseRepository<long, ProductImage>(context), IProductImageRepository;