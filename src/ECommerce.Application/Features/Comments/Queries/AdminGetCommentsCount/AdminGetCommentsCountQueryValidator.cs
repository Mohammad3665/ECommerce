namespace ECommerce.Application.Features.Comments.Queries.AdminGetCommentsCount;

public class AdminGetCommentsCountQueryValidator : AbstractValidator<AdminGetCommentsCountQuery>
{
    public AdminGetCommentsCountQueryValidator()
    {
        RuleFor(x => x)
            .Must(x => (x.ProductId.HasValue && !x.ArticleId.HasValue) || (!x.ProductId.HasValue && x.ArticleId.HasValue))
            .WithMessage("جهت دریافت تعداد دیدگاه‌ها، باید مشخص کنید مربوط به کدام محصول یا مقاله است.");
    }
}
