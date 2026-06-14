using ECommerce.Domain.Entities.Application.Article;
using ECommerce.Domain.IRepositories.Persistence.Application.Article;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Article;

public class ArticleCategoryRepository(ApplicationDbContext context)
    : BaseRepository<long, ArticleCategory>(context), IArticleCategoryRepository;