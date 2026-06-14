using ECommerce.Domain.IRepositories.Persistence.Application.Slide;
using ECommerce.Infrastructure.Repositories.Common.Base;

namespace ECommerce.Infrastructure.Repositories.Persistence.Application.Slide;

public class SlideRepository(ApplicationDbContext context)
    : BaseRepository<long, Domain.Entities.Application.Slide.Slide>(context), ISlideRepository;