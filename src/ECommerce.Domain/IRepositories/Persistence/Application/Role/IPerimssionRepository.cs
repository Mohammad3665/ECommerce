using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Application.Role;

public interface IPerimssionRepository : IBaseRepository<long, Permission>
{
    
    Task<List<long>> GetAllIdsAsync(CancellationToken cancellationToken = default);
}