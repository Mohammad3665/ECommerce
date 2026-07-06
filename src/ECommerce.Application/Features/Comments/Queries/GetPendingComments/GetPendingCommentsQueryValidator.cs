using ECommerce.Application.Common.Validators;
using ECommerce.Domain.Entities.Common;

namespace ECommerce.Application.Features.Comments.Queries.GetPendingComments;

public class GetPendingCommentsQueryValidator : QueryRequestValidator<GetPendingCommentsQuery, Comment>
{
    public GetPendingCommentsQueryValidator() { }
}
