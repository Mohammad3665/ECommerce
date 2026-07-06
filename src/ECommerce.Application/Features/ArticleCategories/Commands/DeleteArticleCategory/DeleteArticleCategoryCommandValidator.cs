namespace ECommerce.Application.Features.ArticleCategories.Commands.DeleteArticleCategory;

public class DeleteArticleCategoryCommandValidator : AbstractValidator<DeleteArticleCategoryCommand>
{
    public DeleteArticleCategoryCommandValidator()
    {
        RuleFor(x => x.Slug)
            .NotEmpty()
            .WithName("اسلاگ")
            .MaximumLength(1000);
    }
}
