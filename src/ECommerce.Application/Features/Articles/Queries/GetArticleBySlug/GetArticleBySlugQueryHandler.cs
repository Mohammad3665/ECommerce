using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Articles.Queries.GetArticleBySlug;

public class GetArticleBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetArticleBySlugQuery, Result<GetArticleResponseDto>>
{
    public async Task<Result<GetArticleResponseDto>> Handle(GetArticleBySlugQuery request, CancellationToken cancellationToken)
    {
        var article = await unitOfWork.ArticleRepository.GetAsync<GetArticleResponseDto>(
            expression: a => a.Slug == request.Slug.Trim().ToLower() && a.Status == ArticleStatus.Published,
            cancellationToken: cancellationToken
        );

        return article is null ?
            new Error("Article.NotFound", "مقاله یافت نشد.", ErrorType.NotFound) :
            article;
    }
}
