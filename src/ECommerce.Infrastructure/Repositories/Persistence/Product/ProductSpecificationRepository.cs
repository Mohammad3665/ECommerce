using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Persistence.Product;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class ProductSpecificationRepository(ApplicationDbContext context)
    : BaseRepository<long, ProductSpecification>(context), IProductSpecificationRepository;