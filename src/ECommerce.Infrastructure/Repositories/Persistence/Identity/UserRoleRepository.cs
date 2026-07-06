using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Persistence.Identity;

namespace ECommerce.Infrastructure.Repositories.Persistence.Identity;

public class UserRoleRepository(ApplicationDbContext context) : IUserRoleRepository
{
    public async Task AddAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        await context.UserRoles.AddAsync(userRole, cancellationToken);
    }

    public async void Remove(UserRole userRole)
    {
        context.UserRoles.Remove(userRole);
    }

    public async Task<UserRole?> GetAsync(Guid userId, long roleId, CancellationToken cancellationToken = default)
    {
        return await context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
    }

    public async Task<List<UserRole>> GetByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.UserRoles
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, long roleId, CancellationToken cancellationToken = default)
    {
        return await context.UserRoles
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
    }

    public async Task<bool> HasAssignedUsersAsync(long roleId, CancellationToken cancellationToken = default)
    {
        return await context.UserRoles.AnyAsync(r => r.RoleId == roleId, cancellationToken);
    }

    public async Task MigrateUsersToRoleAsync(long sourceRoleId, long targetRoleId, CancellationToken cancellationToken = default)
    {
        var userIdsWithSourceRole = await context.UserRoles
            .Where(ur => ur.RoleId == sourceRoleId)
            .Select(ur => ur.UserId)
            .ToListAsync(cancellationToken);

        var userIdsWithTargetRole = await context.UserRoles
            .Where(ur => userIdsWithSourceRole.Contains(ur.UserId) && ur.RoleId == targetRoleId)
            .Select(ur => ur.UserId)
            .ToListAsync(cancellationToken);

        var usersToUpdate = userIdsWithSourceRole.Except(userIdsWithTargetRole).ToList();
        if (usersToUpdate.Any())
        {
            await context.UserRoles
                .Where(ur => ur.RoleId == sourceRoleId && usersToUpdate.Contains(ur.UserId))
                .ExecuteUpdateAsync(s => s.SetProperty(ur => ur.RoleId, targetRoleId), cancellationToken);
        }

        if (userIdsWithTargetRole.Any())
        {
            await context.UserRoles
                .Where(ur => ur.RoleId == sourceRoleId && userIdsWithTargetRole.Contains(ur.UserId))
                .ExecuteDeleteAsync(cancellationToken);
        }
    }

    public async Task DeleteUserRolesByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        await context.UserRoles.Where(ur => ur.UserId == userId).ExecuteDeleteAsync(cancellationToken);
    }

    public async Task AddRangeAsync(List<UserRole> userRoles, CancellationToken cancellationToken = default)
    {
        await context.UserRoles.AddRangeAsync(userRoles, cancellationToken);
    }

}
