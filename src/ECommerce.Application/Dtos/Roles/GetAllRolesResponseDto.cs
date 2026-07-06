using ECommerce.Domain.Entities.Application.Role;

namespace ECommerce.Application.Dtos.Roles;

public record GetAllRolesResponseDto(
    string DisplayName,
    string Description,
    int level
) : IMapFrom<Role>;