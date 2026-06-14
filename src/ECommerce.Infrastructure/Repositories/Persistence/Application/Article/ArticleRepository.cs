using ECommerce.Domain.IRepositories.Persistence.Application.Article;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Article;

public class ArticleRepository(ApplicationDbContext context) 
    : BaseRepository<long, Domain.Entities.Application.Article.Article>(context), IArticleRepository;