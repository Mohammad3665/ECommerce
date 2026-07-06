using ECommerce.Domain.IRepositories.Persistence.Application.Slide;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Slide;

public class SlideRepository(ApplicationDbContext context)
    : BaseRepository<long, Domain.Entities.Application.Slide.Slide>(context), ISlideRepository
{
    public async Task<int> GetMaxOrderAsync(CancellationToken cancellationToken = default)
    {
        var hasAny = await Db.AnyAsync(cancellationToken);
        if (!hasAny) return 0;

        return await Db.MaxAsync(s => s.DisplayOrder, cancellationToken);
    }
}