using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(long Id) : IRequest<Result<CategoryDto>>;
