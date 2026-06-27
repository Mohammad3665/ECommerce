using ECommerce.Application.Dtos.Brands;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Brands.Queries.GetBrandBySlug;

public record GetBrandBySlugQuery(string Slug) : IRequest<Result<GetBrandResponseDto>>;
