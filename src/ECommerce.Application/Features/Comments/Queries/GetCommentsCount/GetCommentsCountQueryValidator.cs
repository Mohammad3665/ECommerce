namespace ECommerce.Application.Features.Comments.Queries.GetCommentsCount;

public class GetCommentsCountQueryValidator : AbstractValidator<GetCommentsCountQuery>
{
    public GetCommentsCountQueryValidator()
    {
        RuleFor(x => x)
            .Must(x => (x.ProductId.HasValue && !x.ArticleId.HasValue) || (!x.ProductId.HasValue && x.ArticleId.HasValue))
            .WithMessage("جهت دریافت تعداد دیدگاه‌ها، باید مشخص کنید مربوط به کدام محصول یا مقاله است.");
    }
}
