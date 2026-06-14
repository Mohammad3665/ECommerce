using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.IRepositories.Persistence.Order;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class OrderPaymentRepository(ApplicationDbContext context)
    : BaseRepository<long, OrderPayment>(context), IOrderPaymentRepository;