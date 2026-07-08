using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Application.Article;
using Mapster;

namespace ECommerce.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<CreateArticleCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var isDuplicateEnglishTitle = await unitOfWork.ArticleRepository.IsExistAsync(
            expression: a => a.EnglishTitle == request.EnglishTitle.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isDuplicateEnglishTitle)
            return new Error("Article.DuplicateName", "مقاله با این نام انگلیسی قبلا در سیستم ثبت شده است.", ErrorType.Validation);

        var article = request.Adapt<Article>();
        article.Slug = request.EnglishTitle.ToSlug();
        article.AuthorId = (Guid)currentUser.UserId!;

        await unitOfWork.ArticleRepository.AddAsync(article, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Article.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            article.Id;
    }
}
