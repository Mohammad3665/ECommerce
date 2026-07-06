namespace ECommerce.Application.Features.Slides.Commands.EditSlide;

public class EditSlideCommandValidator : AbstractValidator<EditSlideCommand>
{
    public EditSlideCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithName("عنوان")
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithName("توضیحات")
            .MaximumLength(300);

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .WithName("آدرس عکس")
            .MaximumLength(300);

        RuleFor(x => x.Link)
            .NotEmpty()
            .WithName("پیوند")
            .MaximumLength(300);

        RuleFor(x => x.DisplayOrder)
            .GreaterThan(0)
            .WithName("اولویت نمایش");
    }
}
