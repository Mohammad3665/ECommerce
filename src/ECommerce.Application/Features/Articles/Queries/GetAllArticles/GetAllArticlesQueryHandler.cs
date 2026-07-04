using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Enums;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

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

        return Result<IEnumerable<GetAllArticlesResponseDto>>.Success(articles);
    }
}