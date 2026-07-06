using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.IRepositories.Persistence.Order;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class OrderItemRepository(ApplicationDbContext context)
    : BaseRepository<Guid, OrderItem>(context), IOrderItemRepository;