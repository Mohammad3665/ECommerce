using ECommerce.Application.Dtos.ArticleCategories;

namespace ECommerce.Application.Features.ArticleCategories.Queries.GetArticleCategoryBySlug;

public class GetArticleCategoryBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetArticleCategoryBySlugQuery, Result<GetArticleCategoryResponseDto>>
{
    public async Task<Result<GetArticleCategoryResponseDto>> Handle(GetArticleCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var articleCategory = await unitOfWork.ArticleCategoryRepository.GetAsync<GetArticleCategoryResponseDto>(
            expression: ac => ac.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );

        return articleCategory is null ?
            new Error("ArticleCategory.NotFound", "دسته‌بندی یافت نشد.", ErrorType.NotFound) :
            articleCategory;
    }
}