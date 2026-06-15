using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(long Id) : IRequest<Result>;
