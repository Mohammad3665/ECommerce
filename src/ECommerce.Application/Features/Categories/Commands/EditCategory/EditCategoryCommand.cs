using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.EditCategory;

public record EditCategoryCommand(
    string Slug,
    string Name,
    string EnglishName,
    string? Description,
    string? ImageUrl
) : IRequest<Result>;