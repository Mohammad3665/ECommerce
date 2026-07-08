using ECommerce.Application.Dtos.Articles;

namespace ECommerce.Application.Features.Articles.Queries.AdminGetArticleBySlug;

public class AdminGetArticleBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<AdminGetArticleBySlugQuery, Result<GetAdminArticleResponseDto>>
{
    public async Task<Result<GetAdminArticleResponseDto>> Handle(AdminGetArticleBySlugQuery request, CancellationToken cancellationToken)
    {
        var article = await unitOfWork.ArticleRepository.GetAsync<GetAdminArticleResponseDto>(
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
            return Result<GetAdminArticleResponseDto>.Failure(error);
        }

        return article;
    }
}