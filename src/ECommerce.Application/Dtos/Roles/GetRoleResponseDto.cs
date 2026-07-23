using ECommerce.Domain.Entities.Application.Role;
using Mapster;

namespace ECommerce.Application.Dtos.Roles;

public record GetRoleResponseDto(
    long Id,
    string Name,
    string DisplayName,
    string Slug,
    string Description,
    int Level,
    bool IsDefault,
    List<long>? PermissionIds
) : IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Role, GetRoleResponseDto>()
            .Map(dest => dest.PermissionIds, src => src.RolePermissions != null
                ? src.RolePermissions.Select(rp => rp.PermissionId).ToList()
                : new List<long>());
    }
}