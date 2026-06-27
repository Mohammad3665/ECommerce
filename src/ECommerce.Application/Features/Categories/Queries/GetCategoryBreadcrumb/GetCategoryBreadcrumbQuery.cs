using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBreadcrumb;

public record GetCategoryBreadcrumbQuery(string Slug) : IRequest<Result<List<BreadcrumbItemResponseDto>>>;
