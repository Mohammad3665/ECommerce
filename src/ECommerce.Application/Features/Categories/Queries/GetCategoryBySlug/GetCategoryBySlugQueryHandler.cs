using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBySlug;

public class GetCategoryBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryBySlugQuery, Result<GetCategoryResponseDto>>
{
    public async Task<Result<GetCategoryResponseDto>> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync<GetCategoryResponseDto>(
            expression: c => c.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (category is null)
        {
            var error = new Error(
                "Category.NotFound",
                "دسته‌بندی مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result<GetCategoryResponseDto>.Failure(error);
        }
        return Result<GetCategoryResponseDto>.Success(category);
    }
}
