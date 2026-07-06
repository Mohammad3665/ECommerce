using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<Result<IEnumerable<GetCategoryResponseDto>>>;
