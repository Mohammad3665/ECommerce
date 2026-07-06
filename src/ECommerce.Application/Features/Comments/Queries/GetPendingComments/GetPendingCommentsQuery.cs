using ECommerce.Application.Dtos.Comments;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Comments.Queries.GetPendingComments;

public class GetPendingCommentsQuery : QueryRequest, IRequest<Result<Pagination<AdminCommentsResponseDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
}
