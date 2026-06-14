using ECommerce.Domain.IRepositories.Persistence.Application.Role;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Role;

public class RoleRepository(ApplicationDbContext context)
    : BaseRepository<long, Domain.Entities.Application.Role.Role>(context), IRoleRepository;