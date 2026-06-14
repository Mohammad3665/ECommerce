using ECommerce.Domain.Entities.Application.Article;
using ECommerce.Domain.IRepositories.Common.Base;

namespace ECommerce.Domain.IRepositories.Persistence.Application.Article;

public interface IArticleCategoryRepository : IBaseRepository<long, ArticleCategory>;