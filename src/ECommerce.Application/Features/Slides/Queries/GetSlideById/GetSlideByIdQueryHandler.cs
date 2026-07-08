using ECommerce.Application.Dtos.Slides;

namespace ECommerce.Application.Features.Slides.Queries.GetSlideById;

public class GetSlideByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetSlideByIdQuery, Result<SlideResponseDto>>
{
    public async Task<Result<SlideResponseDto>> Handle(GetSlideByIdQuery request, CancellationToken cancellationToken)
    {
        var slide = await unitOfWork.SlideRepository.GetAsync<SlideResponseDto>(
            expression: s => s.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (slide is null)
        {
            var error = new Error(
                "Slide.NotFound",
                "اسلاید یافت نشد.",
                ErrorType.NotFound
            );
            return Result<SlideResponseDto>.Failure(error);
        }

        return slide;
    }
}