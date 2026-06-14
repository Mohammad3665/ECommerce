using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Product;

public interface ICommentRepository : IBaseRepository<Guid, Comment>;