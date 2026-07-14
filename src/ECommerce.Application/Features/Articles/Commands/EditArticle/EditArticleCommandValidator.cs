namespace ECommerce.Application.Features.Articles.Commands.EditArticle;

public class EditArticleCommandValidator : AbstractValidator<EditArticleCommand>
{
    public EditArticleCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithName("عنوان")
            .MaximumLength(150);

        RuleFor(x => x.EnglishTitle)
            .NotEmpty()
            .WithName("عنوان انگلیسی")
            .MaximumLength(150);

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithName("محتوا");

        RuleFor(x => x.Summary)
            .NotEmpty()
            .WithName("خلاصه محتوا")
            .MaximumLength(300);

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .WithName("آدرس عکس")
            .MinimumLength(300);

        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(300);
    }
}