using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.IRepositories.Persistence.Application.Role;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Role;

public class PermissionRepository(ApplicationDbContext context)
    : BaseRepository<long, Permission>(context), IPerimssionRepository;