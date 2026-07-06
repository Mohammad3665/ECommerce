using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Persistence.Product;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class BrandRepository(ApplicationDbContext context)
    : BaseRepository<long, Brand>(context), IBrandRepository;