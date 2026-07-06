using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Articles.Commands.ChangeArticleStatus;

public class ChangeArticleStatusCommandValidator : AbstractValidator<ChangeArticleStatusCommand>
{
    public ChangeArticleStatusCommandValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
        
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
