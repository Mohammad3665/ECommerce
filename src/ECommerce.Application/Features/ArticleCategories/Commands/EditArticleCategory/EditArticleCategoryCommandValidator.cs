namespace ECommerce.Application.Features.ArticleCategories.Commands.EditArticleCategory;

public class EditArticleCategoryCommandValidator : AbstractValidator<EditArticleCategoryCommand>
{
    public EditArticleCategoryCommandValidator()
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
