using ECommerce.Application.Dtos.Users;
using ECommerce.Domain.Entities.Identity;
using Mapster;

namespace ECommerce.Application.Features.Users.Mappings;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, GetUserResponseDto>()
            .Map(dest => dest.Roles, src => src.UserRoles.Select(ur => new UserRoleDto(
                ur.Role.Id,
                ur.Role.Name,
                ur.Role.Slug)));
    }
}
