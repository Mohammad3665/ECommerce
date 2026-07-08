using ECommerce.Application.Dtos.Slides;
using ECommerce.Domain.Entities.Application.Slide;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Slides.Queries.GetAdminSlides;

public class GetAdminSlidesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAdminSlidesQuery, Result<Pagination<AdminSlidesResponseDto>>>
{
    public async Task<Result<Pagination<AdminSlidesResponseDto>>> Handle(GetAdminSlidesQuery request, CancellationToken cancellationToken)
    {
        var slides = await unitOfWork.SlideRepository.GetPagedListAsync<AdminSlidesResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );

        return slides;
    }
}