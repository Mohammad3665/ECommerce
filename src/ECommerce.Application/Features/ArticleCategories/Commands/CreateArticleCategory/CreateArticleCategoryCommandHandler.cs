using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Application.Article;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

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
        {
            var error = new Error(
                "ArticleCategory.DuplicateName",
                "دسته‌بندی با این نام انگلیسی قبلا در سیستم ثبت شده است.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }

        var articleCategory = request.Adapt<ArticleCategory>();

        await unitOfWork.ArticleCategoryRepository.AddAsync(articleCategory, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "ArticleCategory.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result<long>.Failure(error);
        }

        return Result<long>.Success(articleCategory.Id);
    }
}