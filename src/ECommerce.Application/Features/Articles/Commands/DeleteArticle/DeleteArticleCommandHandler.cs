using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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
        {
            var error = new Error(
                "Article.NotFound",
                "مقاله یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var imageUrlToRemove = article.ImageUrl;

        unitOfWork.ArticleRepository.DeletePermanently(article);
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

        if (saveResult.IsSuccess && !string.IsNullOrWhiteSpace(imageUrlToRemove))
        {
            fileService.DeleteFile(imageUrlToRemove);
        }

        return Result.Success();
    }
}