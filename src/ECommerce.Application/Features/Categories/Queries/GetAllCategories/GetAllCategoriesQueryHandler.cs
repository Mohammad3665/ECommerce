using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCategoriesQuery, Result<IEnumerable<GetCategoryResponseDto>>>
{
    public async Task<Result<IEnumerable<GetCategoryResponseDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.CategoryRepository.GetAllAsync<GetCategoryResponseDto>(
            expression: null,
            order: query => query.OrderBy(c => c.Name),
            cancellationToken: cancellationToken
        );
        if (categories is null || !categories.Any())
            return new Error("Category.NotFound", "هیچ دیتایی جهت نمایش وجود ندارد.", ErrorType.NotFound);

        return Result<IEnumerable<GetCategoryResponseDto>>.Success(categories);
    }
}