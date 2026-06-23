using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.IRepositories.Persistence.Application.Role;
using ECommerce.Infrastructure.Repositories.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Role;

public class PermissionRepository(ApplicationDbContext context)
    : BaseRepository<long, Permission>(context), IPerimssionRepository
{
    public async Task<List<long>> GetAllIdsAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Permissions.Select(p => p.Id).ToListAsync(cancellationToken);
    }
}