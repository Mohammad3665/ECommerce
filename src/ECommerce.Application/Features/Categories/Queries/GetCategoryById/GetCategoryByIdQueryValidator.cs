using FluentValidation;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
{
    public GetCategoryByIdQueryValidator()
    {
        RuleFor (x => x.Id)
            .NotEmpty()
            .GreaterThan(0)
            .WithName("شناسه دسته‌بندی");
    }
}
