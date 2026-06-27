using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBySlug;

public record GetCategoryBySlugQuery(string Slug) : IRequest<Result<GetCategoryResponseDto>>;
