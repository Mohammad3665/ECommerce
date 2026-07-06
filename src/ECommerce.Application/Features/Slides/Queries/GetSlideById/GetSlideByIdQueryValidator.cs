namespace ECommerce.Application.Features.Slides.Queries.GetSlideById;

public class GetSlideByIdQueryValidator : AbstractValidator<GetSlideByIdQuery>
{
    public GetSlideByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithName("آیدی");
    }
}