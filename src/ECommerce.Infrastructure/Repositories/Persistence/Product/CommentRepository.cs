using ECommerce.Domain.Entities.Common;
using ECommerce.Domain.IRepositories.Common.Comment;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Product;

public class CommentRepository(ApplicationDbContext context) 
    : BaseRepository<Guid, Comment>(context), ICommentRepository;