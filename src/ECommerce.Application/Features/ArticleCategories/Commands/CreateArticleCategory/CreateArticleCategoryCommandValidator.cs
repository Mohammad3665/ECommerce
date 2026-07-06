namespace ECommerce.Application.Features.ArticleCategories.Commands.CreateArticleCategory;

public class CreateArticleCategoryCommandValidator : AbstractValidator<CreateArticleCategoryCommand>
{
    public CreateArticleCategoryCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithName("نام")
            .MaximumLength(150);

        RuleFor(x => x.EnglishName)
            .NotEmpty()
            .WithName("نام انگلیسی")
            .MaximumLength(150);
    }
}
