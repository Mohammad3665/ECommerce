using System.Data;
using FluentValidation;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
    }
}
