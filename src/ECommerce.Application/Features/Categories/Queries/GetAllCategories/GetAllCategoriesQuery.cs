using ECommerce.Application.Common.Mapping;
using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery() : IRequest<Result<IEnumerable<CategoryDto>>>;
