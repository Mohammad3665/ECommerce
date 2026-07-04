using FluentValidation;

namespace ECommerce.Application.Features.Articles.Queries.AdminGetArticleBySlug;

public class AdminGetArticleBySlugQueryValidtor : AbstractValidator<AdminGetArticleBySlugQuery>
{
    public AdminGetArticleBySlugQueryValidtor()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
    }
}