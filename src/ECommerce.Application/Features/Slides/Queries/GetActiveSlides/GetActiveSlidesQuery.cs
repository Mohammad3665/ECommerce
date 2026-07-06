using ECommerce.Application.Dtos.Slides;

namespace ECommerce.Application.Features.Slides.Queries.GetActiveSlides;

public record GetActiveSlidesQuery : IRequest<Result<IEnumerable<SlideResponseDto>>>;
