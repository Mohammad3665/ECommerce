using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.IRepositories.Persistence.Order;

namespace ECommerce.Infrastructure.Repositories.Persistence.Order;

public class CouponRepository(ApplicationDbContext context) 
    : BaseRepository<Guid, Coupon>(context), ICouponRepository;