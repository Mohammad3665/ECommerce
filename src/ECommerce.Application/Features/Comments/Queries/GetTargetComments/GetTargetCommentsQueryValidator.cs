namespace ECommerce.Application.Features.Comments.Queries.GetTargetComments;

public class GetTargetCommentsQueryValidator : AbstractValidator<GetTargetCommentsQuery>
{
    public GetTargetCommentsQueryValidator()
    {
        RuleFor(x => x)
            .Must(x => (x.ProductId.HasValue && !x.ArticleId.HasValue) || (!x.ProductId.HasValue && x.ArticleId.HasValue))
            .WithMessage("دیدگاه باید دقیقاً یا برای یک محصول باشد یا یک مقاله، ارسال هم‌زمان یا خالی گذاشتن هر دو مجاز نیست.");
    }
}
