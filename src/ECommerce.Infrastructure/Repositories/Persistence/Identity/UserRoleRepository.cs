using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.Persistence.Identity;

public class UserRoleRepository(ApplicationDbContext context) : IUserRoleRepository
{
    public async Task AddAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        await context.Set<UserRole>().AddAsync(userRole, cancellationToken);
    }
    
    public async void Remove(UserRole userRole)
    {
        context.Set<UserRole>().Remove(userRole);
    }

    public async Task<UserRole?> GetAsync(Guid userId, long roleId, CancellationToken cancellationToken = default)
    {
        return await context.Set<UserRole>().FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
    }

    public async Task<List<UserRole>> GetByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<UserRole>()
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, long roleId, CancellationToken cancellationToken = default)
    {
        return await context.Set<UserRole>()
            .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, cancellationToken);
    }
}
