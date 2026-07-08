namespace ECommerce.Application.Features.Articles.Commands.DeleteArticle;

public class DeleteArticleCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<DeleteArticleCommand, Result>
{
    public async Task<Result> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await unitOfWork.ArticleRepository.GetAsync(
            expression: a => a.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (article is null)
            return new Error("Article.NotFound", "مقاله یافت نشد.", ErrorType.NotFound);

        var imageUrlToRemove = article.ImageUrl;

        unitOfWork.ArticleRepository.DeletePermanently(article);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        if (saveResult.IsFailure)
            return new Error("Article.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);

        if (saveResult.IsSuccess && !string.IsNullOrWhiteSpace(imageUrlToRemove))
            fileService.DeleteFile(imageUrlToRemove);

        return Result.Success();
    }
}
