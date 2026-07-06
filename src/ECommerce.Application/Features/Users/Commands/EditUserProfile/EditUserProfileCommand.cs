namespace ECommerce.Application.Features.Users.Commands.EditUserProfile;

public record EditUserProfileCommand(string FullName, string PhoneNumber) : IRequest<Result>;
