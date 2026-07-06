using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Persistence.Product;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class ProductSpecificationRepository(ApplicationDbContext context)
    : BaseRepository<long, ProductSpecification>(context), IProductSpecificationRepository;