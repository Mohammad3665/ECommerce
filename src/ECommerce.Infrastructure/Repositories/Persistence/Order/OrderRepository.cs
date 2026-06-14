using ECommerce.Domain.IRepositories.Persistence.Order;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class OrderRepository(ApplicationDbContext context) 
    : BaseRepository<long, Domain.Entities.Order.Order>(context), IOrderRepository;