using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryTree;

public record GetCategoryTreeQuery : IRequest<Result<List<CategoryTreeResponseDto>>>;
