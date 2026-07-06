using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBreadcrumb;

public record GetCategoryBreadcrumbQuery(string Slug) : IRequest<Result<List<BreadcrumbItemResponseDto>>>;
