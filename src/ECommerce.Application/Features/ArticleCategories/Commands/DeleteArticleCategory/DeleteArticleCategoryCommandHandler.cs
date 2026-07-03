using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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
        {
            var error = new Error(
                "ArticleCategory.NotFound",
                "دسته‌بندی یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var hasArticle = await unitOfWork.ArticleRepository.IsExistAsync(
            expression: a => a.ArticleCategoryId == articleCategory.Id,
            cancellationToken: cancellationToken
        );
        if (hasArticle)
        {
            var error = new Error(
                "ArticleCategory.CannotDeleteWithArticle",
                "این دسته‌بندی شامل مقاله است و قابل حذف نیست. لطفا مقالات را به دسته‌بندی دیگری منتقل کنید.",
                ErrorType.Validation
            );
            return Result.Failure(error);
        }

        unitOfWork.ArticleCategoryRepository.DeletePermanently(articleCategory);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "ArticleCategory.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        return Result.Success();
    }
}