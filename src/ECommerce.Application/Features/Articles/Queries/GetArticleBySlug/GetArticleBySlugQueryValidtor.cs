namespace ECommerce.Application.Features.Articles.Queries.GetArticleBySlug;

public class GetArticleBySlugQueryValidtor : AbstractValidator<GetArticleBySlugQuery>
{
    public GetArticleBySlugQueryValidtor()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
    }
}