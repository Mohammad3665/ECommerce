namespace ECommerce.Application.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;

public class GetArticleCategoryBySlugQueryValidator : AbstractValidator<GetArticleCategoryBySlugQuery>
{
    public GetArticleCategoryBySlugQueryValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
    }
}
