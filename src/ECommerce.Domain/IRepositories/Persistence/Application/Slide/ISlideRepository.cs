using System.Linq.Expressions;
using ECommerce.Domain.Entities.Application.Slide;

namespace ECommerce.Domain.IRepositories.Persistence.Application.Slide;

public interface ISlideRepository : IBaseRepository<long, Entities.Application.Slide.Slide>
{
    Task<int> GetMaxOrderAsync(CancellationToken cancellationToken = default);
    Task<List<Entities.Application.Slide.Slide>> GetAllWithTrackingAsync(
        Expression<Func<Entities.Application.Slide.Slide, bool>>? expression = null,
        Func<IQueryable<Entities.Application.Slide.Slide>, IOrderedQueryable<Entities.Application.Slide.Slide>>? order = null,
        CancellationToken cancellationToken = default,
        params Expression<Func<Entities.Application.Slide.Slide, object>>[] includes);
}