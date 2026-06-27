using FluentValidation;

namespace ECommerce.Application.Features.Brands.Commands.EditBrand;

public class EditBrandCommandValidator : AbstractValidator<EditBrandCommand>
{
    public EditBrandCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithName("نام برند")
            .MaximumLength(100);

        RuleFor(x => x.EnglishName)
            .NotEmpty()
            .WithName("نام انگلیسی")
            .MaximumLength(100)
            .Matches("^[a-zA-Z0-9\\s-_]+$");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithName("توضیحات")
            .MaximumLength(1500);

        RuleFor(x => x.LogoImageUrl)
            .NotEmpty()
            .WithName("عکس")
            .MaximumLength(300);
    }
}
