using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryTree;

public record GetCategoryTreeQuery : IRequest<Result<List<CategoryTreeDto>>>;
