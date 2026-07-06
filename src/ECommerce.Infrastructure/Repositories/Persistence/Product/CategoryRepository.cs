using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Persistence.Product;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class CategoryRepository(ApplicationDbContext context)
    : BaseRepository<long, Category>(context), ICategoryRepository;