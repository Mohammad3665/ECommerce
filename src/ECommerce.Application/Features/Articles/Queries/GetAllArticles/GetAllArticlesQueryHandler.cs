using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Articles.Queries.GetAllArticles;

public class GetAllArticlesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllArticlesQuery, Result<IEnumerable<GetAllArticlesResponseDto>>>
{
    public async Task<Result<IEnumerable<GetAllArticlesResponseDto>>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await unitOfWork.ArticleRepository.GetAllAsync<GetAllArticlesResponseDto>(
            expression: a => a.Status == ArticleStatus.Published,
            order: query => query.OrderBy(a => a.Title),
            cancellationToken: cancellationToken
        );
        if (articles is null || !articles.Any())
            return new Error("Article.NotFound", "هیچ دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<GetAllArticlesResponseDto>>.Success(articles);
    }
}
