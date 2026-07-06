using ECommerce.Domain.IRepositories.Persistence.Order;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class OrderRepository(ApplicationDbContext context) 
    : BaseRepository<long, Domain.Entities.Order.Order>(context), IOrderRepository;