using FluentValidation;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBreadcrumb;

public class GetCategoryBreadcrumbQueryValidator : AbstractValidator<GetCategoryBreadcrumbQuery>
{
    public GetCategoryBreadcrumbQueryValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاک");
    }
}
