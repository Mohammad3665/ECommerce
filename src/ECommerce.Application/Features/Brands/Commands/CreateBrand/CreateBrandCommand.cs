using ECommerce.Application.Common.Mapping;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using MediatR;

namespace ECommerce.Application.Features.Brands.Commands.CreateBrand;

public record CreateBrandCommand(string Name, string EnglishName, string Description, string LogoImageUrl, bool IsActive)
    : IRequest<Result<long>>, IMapTo<Brand>;
