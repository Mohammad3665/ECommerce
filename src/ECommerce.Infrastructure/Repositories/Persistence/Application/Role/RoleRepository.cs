using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Persistence.Application.Role;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Role;

public class RoleRepository
    : BaseRepository<long, Domain.Entities.Application.Role.Role>, IRoleRepository
{
    private readonly ApplicationDbContext context;
    public RoleRepository(ApplicationDbContext context)
        : base(context)
    {
        this.context = context;
    }

    public async Task<List<UserRole>> GetUserRolesByRoleIdAsync(long roleId, CancellationToken cancellationToken)
    {
        return await context.UserRoles.Where(ur => ur.RoleId == roleId).ToListAsync(cancellationToken);
    }

    public void UpdateUserRoles(List<UserRole> userRoles)
    {
        context.UserRoles.UpdateRange(userRoles);
    }
}