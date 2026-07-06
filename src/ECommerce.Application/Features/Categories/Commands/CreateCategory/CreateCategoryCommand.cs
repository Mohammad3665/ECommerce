using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string EnglishName,
    string? Description,
    string? ImageUrl,
    long? ParentCategoryId
) : IRequest<Result<long>>, IMapTo<Category>;