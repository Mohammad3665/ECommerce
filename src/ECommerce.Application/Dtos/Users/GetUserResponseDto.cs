using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Entities.Identity;
using Mapster;

namespace ECommerce.Application.Dtos.Users;

public record GetUserResponseDto(
    Guid Id,
    string FullName,
    string Email,
    string PhoneNumber,
    DateTime? LastLoginAt,
    bool IsActive,
    bool IsEmailConfirmed,
    List<UserRoleDto> Roles
) : IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<User, GetUserResponseDto>()
            .Map(dest => dest.Roles, src => src.UserRoles != null 
                ? src.UserRoles.Select(ur => new UserRoleDto(
                    ur.Role.Id, 
                    ur.Role.Name ?? string.Empty, 
                    ur.Role.Slug ?? string.Empty)) 
                : new List<UserRoleDto>());
    }
}