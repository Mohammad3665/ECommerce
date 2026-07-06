using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
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

        RuleFor(x => x.Status)
            .NotEmpty()
            .IsEnumName(typeof(ArticleStatus), caseSensitive: false)
            .WithMessage("وضعیت ارسال شده معتبر نیست. مقادیر مجاز: " + GetValidStatuses());
    }

    private static string GetValidStatuses()
    {
        var names = Enum.GetNames(typeof(ArticleStatus));
        return string.Join(", ", names);
    }
}
