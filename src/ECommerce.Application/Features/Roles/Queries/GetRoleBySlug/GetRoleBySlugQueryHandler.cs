using ECommerce.Application.Dtos.Roles;

namespace ECommerce.Application.Features.Roles.Queries.GetRoleBySlug;

public class GetRoleBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetRoleBySlugQuery, Result<GetRoleResponseDto>>
{
    public async Task<Result<GetRoleResponseDto>> Handle(GetRoleBySlugQuery request, CancellationToken cancellationToken)
    {
        var role = await unitOfWork.RoleRepository.GetAsync<GetRoleResponseDto>(
            expression: r => r.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken,
            includes: r => r.RolePermissions
        );
        if (role is null)
        {
            var error = new Error(
                "Role.NotFound",
                "نقش مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result<GetRoleResponseDto>.Failure(error);
        }

        return role;
    }
}
