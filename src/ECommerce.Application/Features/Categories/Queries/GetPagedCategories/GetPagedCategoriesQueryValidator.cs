using FluentValidation;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public class GetPagedCategoriesQueryValidator : AbstractValidator<GetPagedCategoriesQuery>
{
    public GetPagedCategoriesQueryValidator()
    {
        RuleFor (x => x.PageNumber)
            .GreaterThan(0)
            .WithName("شماره صفحه");

        RuleFor (x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithName("تعداد آیتم‌ها");
        
        RuleFor(x => x.SearchTerm)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.SearchTerm));
    }
}
