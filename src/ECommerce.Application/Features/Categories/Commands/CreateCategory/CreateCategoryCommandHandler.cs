using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MapsterMapper;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var isDuplicateEnglishName = await unitOfWork.CategoryRepository.IsExistAsync(
            expression: c => c.EnglishName.ToLower() == request.EnglishName.Trim().ToLower(),
            cancellationToken: cancellationToken
        );
        if (isDuplicateEnglishName)
        {
            var error = new Error(
                "Category.DuplicateName",
                "دسته‌بندی با این نام انگلیسی قبلا در سیستم ثبت شده است.",
                ErrorType.Validation
            );
            return Result<long>.Failure(error);
        }
        var category = request.Adapt<Category>();

        await unitOfWork.CategoryRepository.AddAsync(category, cancellationToken);

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Category.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result<long>.Failure(error);
        }
        return Result<long>.Success(category.Id);
    }
}
