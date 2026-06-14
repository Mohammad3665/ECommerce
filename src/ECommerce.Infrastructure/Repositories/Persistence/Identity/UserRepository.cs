using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.IRepositories.Persistence.Identity;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Identity;

public class UserRepository(ApplicationDbContext context) 
    : BaseRepository<Guid, User>(context), IUserRepository;