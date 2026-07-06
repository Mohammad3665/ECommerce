using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.IRepositories.Persistence.Order;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class OrderShippingRepository(ApplicationDbContext context)
    : BaseRepository<long, OrderShipping>(context), IOrderShippingRepository;