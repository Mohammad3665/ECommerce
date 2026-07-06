using ECommerce.Domain.IRepositories.Persistence.Application.Article;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Article;

public class ArticleRepository(ApplicationDbContext context) 
    : BaseRepository<long, Domain.Entities.Application.Article.Article>(context), IArticleRepository;