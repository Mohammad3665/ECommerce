using System.Data;
using FluentValidation;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithName("نام دسته‌بندی")
            .MaximumLength(150);

        RuleFor(x => x.EnglishName)
            .NotEmpty()
            .WithName("نام انگلیسی")
            .MaximumLength(150)
            .Matches("^[a-zA-Z0-9\\s-_]+$");
        
        RuleFor(x => x.Description)
            .MinimumLength(10)
            .WithName("توضیحات");
            
    }
}
