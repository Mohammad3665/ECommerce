using ECommerce.Application.Dtos.Articles;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Articles.Queries.GetPagedArticles;

public class GetPagedArticlesQuery : QueryRequest, IRequest<Result<Pagination<GetPagedArticlesResponseDto>>>
{
    int PageNumber { get; set; }
    int PageSize { get; set; }
    string? SearchTerm { get; set; } = string.Empty;
}