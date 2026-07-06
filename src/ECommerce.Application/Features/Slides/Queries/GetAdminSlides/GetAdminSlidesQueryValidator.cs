using ECommerce.Application.Common.Validators;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Entities.Application.Slide;

namespace ECommerce.Application.Features.Slides.Queries.GetAdminSlides;

public class GetAdminSlidesQueryValidator : QueryRequestValidator<GetAdminSlidesQuery, Slide>
{
    public GetAdminSlidesQueryValidator() { }
}
