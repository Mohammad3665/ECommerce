namespace ECommerce.Application.Features.Slides.Commands.ToggleSlideStatus;

public record ToggleSlideStatusCommand(long Id, bool IsActive) : IRequest<Result>;
