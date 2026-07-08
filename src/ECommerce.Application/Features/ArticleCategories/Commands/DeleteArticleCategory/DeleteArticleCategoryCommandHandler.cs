namespace ECommerce.Application.Features.ArticleCategories.Commands.DeleteArticleCategory;

public class DeleteArticleCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteArticleCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await unitOfWork.ArticleCategoryRepository.GetAsync(
            expression: ac => ac.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (articleCategory is null)
            return new Error(
                "ArticleCategory.NotFound", 
                "دسته‌بندی یافت نشد.", 
                ErrorType.NotFound
            );

        var hasArticle = await unitOfWork.ArticleRepository.IsExistAsync(
            expression: a => a.ArticleCategoryId == articleCategory.Id,
            cancellationToken: cancellationToken
        );
        if (hasArticle)
            return new Error(
                "ArticleCategory.CannotDeleteWithArticle", 
                "این دسته‌بندی شامل مقاله است و قابل حذف نیست. لطفا مقالات را به دسته‌بندی دیگری منتقل کنید.", 
                ErrorType.Validation
            );

        unitOfWork.ArticleCategoryRepository.DeletePermanently(articleCategory);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            Result.Failure(new Error("ArticleCategory.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected)) :
            Result.Success();
    }
}