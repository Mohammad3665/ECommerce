using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(long Id) : IRequest<Result<GetCategoryResponseDto>>;
