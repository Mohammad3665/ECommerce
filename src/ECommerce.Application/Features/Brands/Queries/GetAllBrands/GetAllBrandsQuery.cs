using ECommerce.Application.Dtos.Brands;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Brands.Queries.GetAllBrands;

public record GetAllBrandsQuery() : IRequest<Result<IEnumerable<GetBrandResponseDto>>>;
