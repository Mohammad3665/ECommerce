using ECommerce.Application.Dtos.Comments;
using ECommerce.Domain.Common.Enums;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Comments.Queries.GetPendingComments;

public class GetPendingCommentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPendingCommentsQuery, Result<Pagination<AdminCommentsResponseDto>>>
{
    public async Task<Result<Pagination<AdminCommentsResponseDto>>> Handle(GetPendingCommentsQuery request, CancellationToken cancellationToken)
    {
        request.Filters ??= new List<FilterCondition>();
        var filterCondition = new FilterCondition
        {
            Property = "IsApproved",
            Operator = FilterOperator.Equals,
            Value = "false"
        };

        request.Filters.Add(filterCondition);
        var comments = await unitOfWork.CommentRepository.GetPagedListAsync<AdminCommentsResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );
        if (comments is null)
            return new Error("Comment.NotFound", "دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return comments;
    }
}