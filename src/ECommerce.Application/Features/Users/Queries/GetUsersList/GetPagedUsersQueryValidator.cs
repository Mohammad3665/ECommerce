using ECommerce.Application.Common.Validators;
using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Features.Users.Queries.GetUsersList;

public class GetPagedUsersQueryValidator : QueryRequestValidator<GetPagedUsersQuery, User>
{
    public GetPagedUsersQueryValidator() { }
}
