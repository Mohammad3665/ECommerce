using System.Linq.Expressions;
using ECommerce.Domain.IRepositories.Persistence.Application.Slide;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Slide;

public class SlideRepository(ApplicationDbContext context)
    : BaseRepository<long, Domain.Entities.Application.Slide.Slide>(context), ISlideRepository
{
    public async Task<List<Domain.Entities.Application.Slide.Slide>> GetAllWithTrackingAsync(Expression<Func<Domain.Entities.Application.Slide.Slide, bool>>? expression = null, Func<IQueryable<Domain.Entities.Application.Slide.Slide>, IOrderedQueryable<Domain.Entities.Application.Slide.Slide>>? order = null, CancellationToken cancellationToken = default, params Expression<Func<Domain.Entities.Application.Slide.Slide, object>>[] includes)
    {
        var query = BuildQuery(expression, order, false, false, includes);
        return await query.ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<int> GetMaxOrderAsync(CancellationToken cancellationToken = default)
    {
        var hasAny = await Db.AnyAsync(cancellationToken);
        if (!hasAny) return 0;

        return await Db.MaxAsync(s => s.DisplayOrder, cancellationToken);
    }
}