using ECommerce.Domain.Entities.Order;
using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Order;

public interface ICouponRepository : IBaseRepository<Guid, Coupon>;