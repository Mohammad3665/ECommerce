using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Application.Dtos.Users;

public record GetAllUsersResponseDto(Guid Id, string FullName, string Email, string PhoneNumber) : IMapFrom<User>;
