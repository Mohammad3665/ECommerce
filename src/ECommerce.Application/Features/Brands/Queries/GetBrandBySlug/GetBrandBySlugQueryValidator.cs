namespace ECommerce.Application.Features.Brands.Queries.GetBrandBySlug;

public class GetBrandBySlugQueryValidator : AbstractValidator<GetBrandBySlugQuery>
{
    public GetBrandBySlugQueryValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(300);
    }
}
