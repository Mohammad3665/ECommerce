using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBySlug;

public record GetCategoryBySlugQuery(string Slug) : IRequest<Result<GetCategoryResponseDto>>;
