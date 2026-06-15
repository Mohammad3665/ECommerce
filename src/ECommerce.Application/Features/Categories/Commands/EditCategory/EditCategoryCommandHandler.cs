using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.EditCategory;

public class EditCategoryCommandHandler(IUnitOfWorkTransactionHandler handler) : IRequestHandler<EditCategoryCommand, Result>
{
    public async Task<Result> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        => await handler.ExecuteAsync(async (uow, token) =>
        {
            var category = await uow.CategoryRepository.GetAsync(
                expression: c => c.Id == request.Id,
                cancellationToken: token);
            
            if (category is null)
            {
                var error = new Error(
                    "Category.NotFound",
                    $"Category with Id {request.Id} was not found.",
                    ErrorType.NotFound
                );
                return Result.Failure(error);
            }

            var config = new TypeAdapterConfig();
            config.NewConfig<EditCategoryCommand, Category>()
                .IgnoreNullValues(true);
            request.Adapt(category, config);

            uow.CategoryRepository.Update(category);

            var saveResult = await uow.SaveAsync(token);
            if (saveResult.IsFailure)
            {
                var error = new Error(
                    "Operation failed.",
                    "An unexpected error occurred while updating the category.",
                    ErrorType.Unexpected
                );
                return Result.Failure(error);
            }
            return Result.Success();
        }, cancellationToken);
}
