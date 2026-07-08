using ECommerce.Application.Dtos.Slides;

namespace ECommerce.Application.Features.Slides.Queries.GetActiveSlides;

public class GetActiveSlidesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetActiveSlidesQuery, Result<IEnumerable<SlideResponseDto>>>
{
    public async Task<Result<IEnumerable<SlideResponseDto>>> Handle(GetActiveSlidesQuery request, CancellationToken cancellationToken)
    {
        var slides = await unitOfWork.SlideRepository.GetAllAsync<SlideResponseDto>(
            expression: s => s.IsActive,
            cancellationToken: cancellationToken
        );
        if (slides is null || !slides.Any())
            return new Error("Product.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<SlideResponseDto>>.Success(slides);
    }
}
