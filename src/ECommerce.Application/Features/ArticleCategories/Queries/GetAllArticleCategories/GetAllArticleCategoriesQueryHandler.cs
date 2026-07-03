using ECommerce.Application.Dtos.ArticleCategories;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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

        return Result<IEnumerable<GetArticleCategoryResponseDto>>.Success(articleCategories);
    }
}