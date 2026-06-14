using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Application.Role;

public interface IRolePermissionRepository : IBaseRepository<long, RolePermission>;