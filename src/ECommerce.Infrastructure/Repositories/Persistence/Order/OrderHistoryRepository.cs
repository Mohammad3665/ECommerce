using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.IRepositories.Persistence.Order;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class OrderHistoryRepository(ApplicationDbContext context)
    : BaseRepository<long, OrderHistory>(context), IOrderHistoryRepository;