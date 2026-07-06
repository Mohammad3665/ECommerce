namespace ECommerce.Application.Features.Brands.Commands.DeleteBrand;

public record DeleteBrandCommand(string Slug) : IRequest<Result>;
