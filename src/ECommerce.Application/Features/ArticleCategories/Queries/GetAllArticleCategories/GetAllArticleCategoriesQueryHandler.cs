using ECommerce.Application.Dtos.ArticleCategories;

namespace ECommerce.Application.Features.ArticleCategories.Queries.GetAllArticleCategories;

public class GetAllArticleCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllArticleCategoriesQuery, Result<IEnumerable<GetArticleCategoryResponseDto>>>
{
    public async Task<Result<IEnumerable<GetArticleCategoryResponseDto>>> Handle(GetAllArticleCategoriesQuery request, CancellationToken cancellationToken)
    {
        var articleCategories = await unitOfWork.ArticleCategoryRepository.GetAllAsync<GetArticleCategoryResponseDto>(
            expression: null,
            order: query => query.OrderBy(ac => ac.Name),
            cancellationToken: cancellationToken
        );

        return articleCategories is null || !articleCategories.Any() ?
            new Error("ArticleCategory.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound) :
            Result<IEnumerable<GetArticleCategoryResponseDto>>.Success(articleCategories);
    }
}