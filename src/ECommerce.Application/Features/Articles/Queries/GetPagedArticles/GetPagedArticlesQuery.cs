using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Specifications.Common;
using MediatR;

namespace ECommerce.Application.Features.Articles.Queries.GetPagedArticles;

public class GetPagedArticlesQuery : QueryRequest, IRequest<Result<Pagination<GetPagedArticlesResponseDto>>>
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
    string? SearchTerm { get; set; } = string.Empty;
}