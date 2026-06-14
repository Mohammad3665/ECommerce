using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Order;

public interface IOrderRepository : IBaseRepository<long, Entities.Order.Order>;