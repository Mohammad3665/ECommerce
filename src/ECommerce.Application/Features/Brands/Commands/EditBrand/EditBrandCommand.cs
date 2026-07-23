namespace ECommerce.Application.Features.Brands.Commands.EditBrand;

public record EditBrandCommand(string Slug, string Name, string EnglishName, string Description, string LogoImageUrl, bool IsActive)
    : IRequest<Result>;
