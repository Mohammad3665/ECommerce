using ECommerce.Application.Dtos.Categories;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryResponseDto>>
{
    public async Task<Result<GetCategoryResponseDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryDto = await unitOfWork.CategoryRepository.GetAsync<GetCategoryResponseDto>(
            expression: c => c.Id == request.Id,
            cancellationToken: cancellationToken
        );

        if (categoryDto is null)
        {
            var error = new Error(
                "Category.NotFound",
                "دسته‌بندی مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result<GetCategoryResponseDto>.Failure(error);
        }
        return categoryDto;
    }
}