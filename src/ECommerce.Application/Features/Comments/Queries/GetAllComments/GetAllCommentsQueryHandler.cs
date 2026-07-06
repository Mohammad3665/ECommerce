using ECommerce.Application.Dtos.Comments;
using ECommerce.Domain.Common.Enums;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Comments.Queries.GetAllComments;

public class GetAllCommentsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCommentsQuery, Result<Pagination<AdminCommentsResponseDto>>>
{
    public async Task<Result<Pagination<AdminCommentsResponseDto>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        if (request.IsApproved.HasValue)
        {
            request.Filters ??= new List<FilterCondition>();
            var filterCondition = new FilterCondition
            {
                Property = "IsApproved",
                Operator = FilterOperator.Equals,
                Value = request.IsApproved.Value.ToString().ToLower()
            };
            request.Filters.Add(filterCondition);
        }

        var comments = await unitOfWork.CommentRepository.GetPagedListAsync<AdminCommentsResponseDto>(
            request: request,
            cancellationToken: cancellationToken
        );

        return Result<Pagination<AdminCommentsResponseDto>>.Success(comments);
    }
}