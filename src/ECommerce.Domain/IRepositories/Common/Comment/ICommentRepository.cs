using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Common.Comment;

public interface ICommentRepository : IBaseRepository<Guid, Entities.Common.Comment>;