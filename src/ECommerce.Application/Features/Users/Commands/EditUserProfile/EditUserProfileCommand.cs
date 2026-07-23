namespace ECommerce.Application.Features.Users.Commands.EditUserProfile;

public record EditUserProfileCommand(Guid UserId, string FullName, string PhoneNumber) : IRequest<Result>;
