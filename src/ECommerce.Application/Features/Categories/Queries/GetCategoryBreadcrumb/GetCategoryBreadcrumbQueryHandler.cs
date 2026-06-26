using ECommerce.Application.Dtos.Categories;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using MediatR;

namespace ECommerce.Application.Features.Categories.Queries.GetCategoryBreadcrumb;

public class GetCategoryBreadcrumbQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryBreadcrumbQuery, Result<List<BreadcrumbItemDto>>>
{
    public async Task<Result<List<BreadcrumbItemDto>>> Handle(GetCategoryBreadcrumbQuery request, CancellationToken cancellationToken)
    {
        var breadcrumb = new List<BreadcrumbItemDto>();

        var currentCategory = await unitOfWork.CategoryRepository.GetAsync(
            expression: c => c.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (currentCategory is null)
        {
            var error = new Error(
                "Category.NotFound",
                "دسته‌بندی مورد نظر یافت نشد.",
                ErrorType.NotFound
            );
            return Result<List<BreadcrumbItemDto>>.Failure(error);
        }

        var category = currentCategory;
        while (category is not null)
        {
            var item = new BreadcrumbItemDto(category.Id, category.Name, category.Slug);
            breadcrumb.Insert(0, item);

            if (category.ParentCategoryId is null)
                break;

            category = await unitOfWork.CategoryRepository.GetAsync(
                expression: c => c.Id == category.ParentCategoryId && c.IsActive,
                cancellationToken: cancellationToken
            );
        }
        return Result<List<BreadcrumbItemDto>>.Success(breadcrumb);
    }
}
