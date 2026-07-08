using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBySlug;

public class GetCategoryBySlugQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryBySlugQuery, Result<GetCategoryResponseDto>>
{
    public async Task<Result<GetCategoryResponseDto>> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync<GetCategoryResponseDto>(
            expression: c => c.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );

        return category is null ?
            new Error("Category.NotFound", "دسته‌بندی مورد نظر یافت نشد.", ErrorType.NotFound) :
            category;
    }
}
