using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Identity;

public interface IUserRepository : IBaseRepository<Guid, User>;