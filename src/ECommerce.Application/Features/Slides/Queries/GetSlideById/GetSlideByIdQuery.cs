using ECommerce.Application.Dtos.Slides;

namespace ECommerce.Application.Features.Slides.Queries.GetSlideById;

public record GetSlideByIdQuery(long Id) : IRequest<Result<SlideResponseDto>>;
