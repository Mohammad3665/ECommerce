using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.MoveCategory;

public record MoveCategoryCommand(string Slug, long? NewParentId) : IRequest<Result>;
