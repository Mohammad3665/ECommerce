using ECommerce.Domain.Entities.Product;

namespace ECommerce.Application.Features.Brands.Commands.CreateBrand;

public record CreateBrandCommand(string Name, string EnglishName, string Description, string LogoImageUrl, bool IsActive)
    : IRequest<Result<long>>, IMapTo<Brand>;
