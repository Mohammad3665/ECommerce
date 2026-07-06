using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Articles.Queries.GetArticleBySlug;

public class GetArticleBySlugQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser) : IRequestHandler<GetArticleBySlugQuery, Result<GetArticleResponseDto>>
{
    public async Task<Result<GetArticleResponseDto>> Handle(GetArticleBySlugQuery request, CancellationToken cancellationToken)
    {
        var article = await unitOfWork.ArticleRepository.GetAsync<GetArticleResponseDto>(
            expression: a => a.Slug == request.Slug.Trim().ToLower() && a.Status == ArticleStatus.Published,
            cancellationToken: cancellationToken
        );
        if (article is null)
        {
            var error = new Error(
                "Article.NotFound",
                "مقاله یافت نشد.",
                ErrorType.NotFound
            );
            return Result<GetArticleResponseDto>.Failure(error);
        }        

        return Result<GetArticleResponseDto>.Success(article);
    }
}