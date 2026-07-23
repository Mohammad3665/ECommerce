using ECommerce.Domain.Entities.Application.Role;

namespace ECommerce.Application.Dtos.Roles;

public record GetAllRolesResponseDto(
    long Id,
    string Name,
    string DisplayName,
    string Slug,
    string Description,
    int Level,
    bool IsDefault
) : IMapFrom<Role>;