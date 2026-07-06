using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.IRepositories.Persistence.Order;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class OrderPaymentRepository(ApplicationDbContext context)
    : BaseRepository<long, OrderPayment>(context), IOrderPaymentRepository;