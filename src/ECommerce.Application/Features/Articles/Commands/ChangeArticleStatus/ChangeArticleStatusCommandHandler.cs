using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Articles.Commands.ChangeArticleStatus;

public class ChangeArticleStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ChangeArticleStatusCommand, Result>
{
    public async Task<Result> Handle(ChangeArticleStatusCommand request, CancellationToken cancellationToken)
    {
        var article = await unitOfWork.ArticleRepository.GetAsync(
            expression: a => a.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (article is null)
        {
            var error = new Error(
                "Article.NotFound",
                "مقاله یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var targetStatus = Enum.Parse<ArticleStatus>(request.Status);

        if (article.Status != ArticleStatus.Draft && targetStatus == ArticleStatus.Draft)
        {
            var error = new Error(
                "Article.InvalidStatus",
                "مقاله‌ای که منتشتر یا آرشیو شده نمی‌تواند به وضعیت پیش‌نویس بازگردد.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        if (article.Status == targetStatus)
        {
            var error = new Error(
                "Article.SameStatus",
                "مقاله هم‌اکنون در همین وضعیت قرار دارد.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        article.Status = targetStatus;
        article.PublishedAt = targetStatus.Equals(ArticleStatus.Published) ? DateTime.UtcNow : null;
        article.ArchivedAt = targetStatus.Equals(ArticleStatus.Archived) ? DateTime.UtcNow : null;

        unitOfWork.ArticleRepository.Update(article);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Article.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        return Result.Success();
    }
}