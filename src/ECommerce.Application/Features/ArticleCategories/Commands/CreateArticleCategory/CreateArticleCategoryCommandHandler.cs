using ECommerce.Domain.Entities.Application.Article;
using Mapster;

namespace ECommerce.Application.Features.ArticleCategories.Commands.CreateArticleCategory;

public class CreateArticleCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateArticleCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var isDuplicateEnglishName = await unitOfWork.ArticleCategoryRepository.IsExistAsync(
            expression: ac => ac.EnglishName.ToLower() == request.EnglishName.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isDuplicateEnglishName)
            return new Error(
                "ArticleCategory.DuplicateName",
                "دسته‌بندی با این نام انگلیسی قبلا در سیستم ثبت شده است.",
                ErrorType.Validation
            );

        var articleCategory = request.Adapt<ArticleCategory>();

        await unitOfWork.ArticleCategoryRepository.AddAsync(articleCategory, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("ArticleCategory.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            articleCategory.Id;
    }
}