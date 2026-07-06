using ECommerce.Application.Dtos.Brands;

namespace ECommerce.Application.Features.Brands.Queries.GetAllBrands;

public record GetAllBrandsQuery() : IRequest<Result<IEnumerable<GetBrandResponseDto>>>;
