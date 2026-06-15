using ECommerce.Application.Common.Extensions;
using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    string EnglishName,
    string? Description,
    string? ImageUrl,
    long? ParentCategoryId
) : IRequest<Result<long>>, IMapTo<Category>;