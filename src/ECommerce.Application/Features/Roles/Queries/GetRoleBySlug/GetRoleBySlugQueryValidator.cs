namespace ECommerce.Application.Features.Roles.Queries.GetRoleBySlug;

public class GetRoleBySlugQueryValidator : AbstractValidator<GetRoleBySlugQuery>
{
    public GetRoleBySlugQueryValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(300);
    }
}
