using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries.GetProductBySlug;

public record GetProductBySlugQuery(string Slug) : IRequest<Result<GetProductResponseDto>>;
