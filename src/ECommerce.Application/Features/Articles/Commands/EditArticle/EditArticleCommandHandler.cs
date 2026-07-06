using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Entities.Application.Article;
using Mapster;

namespace ECommerce.Application.Features.Articles.Commands.EditArticle;

public class EditArticleCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<EditArticleCommand, Result>
{
    public async Task<Result> Handle(EditArticleCommand request, CancellationToken cancellationToken)
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

        var oldImagePath = article.ImageUrl;
        var hasNewImage = request.ImageUrl != null;

        article.Slug = request.EnglishTitle.ToSlug();
        var config = new TypeAdapterConfig();
        config.NewConfig<EditArticleCommand, Article>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.Slug)
            .Ignore(dest => dest.ImageUrl);
        request.Adapt(article, config);

        if (hasNewImage)
        {
            article.ImageUrl = request.ImageUrl!;
        }

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

        if (hasNewImage && !string.IsNullOrEmpty(oldImagePath))
        {
            fileService.DeleteFile(oldImagePath);
        }

        return Result.Success();
    }
}