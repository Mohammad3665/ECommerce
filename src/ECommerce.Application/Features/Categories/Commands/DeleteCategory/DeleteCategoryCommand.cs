namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(string Slug) : IRequest<Result>;
