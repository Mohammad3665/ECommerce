using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.EditCategory;

public class EditCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<EditCategoryCommand, Result>
{
    public async Task<Result> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(
            expression: c => c.Slug == request.Slug.Trim().ToLower(),
            cancellationToken: cancellationToken);

        if (category is null)
        {
            var error = new Error(
                "Category.NotFound",
                $"دسته‌بندی یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        var config = new TypeAdapterConfig();
        config.NewConfig<EditCategoryCommand, Category>()
            .IgnoreNullValues(true);
        request.Adapt(category, config);

        unitOfWork.CategoryRepository.Update(category);

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Category.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }
        return Result.Success();
    }
}
