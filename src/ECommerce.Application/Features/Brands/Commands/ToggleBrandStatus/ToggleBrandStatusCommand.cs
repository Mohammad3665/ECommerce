using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Brands.Commands.ToggleBrandStatus;

public record ToggleBrandStatusCommand(string Slug, bool IsActive) : IRequest<Result>;
