using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Articles.Queries.GetPagedArticles;

public class GetPagedArticlesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPagedArticlesQuery, Result<Pagination<GetPagedArticlesResponseDto>>>
{
    public async Task<Result<Pagination<GetPagedArticlesResponseDto>>> Handle(GetPagedArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await unitOfWork.ArticleRepository.GetPagedListAsync<GetPagedArticlesResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );

        return Result<Pagination<GetPagedArticlesResponseDto>>.Success(articles);
    }
}