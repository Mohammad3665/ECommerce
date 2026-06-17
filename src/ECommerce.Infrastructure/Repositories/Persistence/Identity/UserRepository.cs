using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Persistence.Identity;
using ECommerce.Infrastructure.Repositories.Common.Base;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.Persistence.Identity;

public class UserRepository(ApplicationDbContext context)

    : BaseRepository<Guid, User>(context), IUserRepository
{
    public async Task<User?> GetUserWithRolesByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserWithRolesByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
}