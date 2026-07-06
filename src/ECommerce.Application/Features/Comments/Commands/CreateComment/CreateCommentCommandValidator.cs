namespace ECommerce.Application.Features.Comments.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Content)
            .NotEmpty()
            .MaximumLength(1000);

        RuleFor(x => x)
            .Must(x => (x.ProductId.HasValue && !x.ArticleId.HasValue) || (!x.ProductId.HasValue && x.ArticleId.HasValue))
            .WithMessage("دیدگاه باید دقیقاً یا برای یک محصول باشد یا یک مقاله، ارسال هم‌زمان یا خالی گذاشتن هر دو مجاز نیست.");
    }
}