namespace ECommerce.Application.Features.Categories.Commands.ToggleCategoryStatus;

public record ToggleCategoryStatusCommand(string Slug, bool IsActive) : IRequest<Result>;
