using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.EditCategory;

public record EditCategoryCommand(
    long Id,
    string Name,
    string EnglishName,
    string? Description,
    string? ImageUrl
) : IRequest<Result>;