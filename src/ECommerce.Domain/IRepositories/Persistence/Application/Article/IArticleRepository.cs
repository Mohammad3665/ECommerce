using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Application.Article;

public interface IArticleRepository : IBaseRepository<long, Entities.Application.Article.Article>;