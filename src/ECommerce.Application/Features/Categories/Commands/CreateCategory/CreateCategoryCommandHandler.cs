using ECommerce.Application.Common.Extensions;
using ECommerce.Domain.Common.Error;
using ECommerce.Domain.Common.Result;
using ECommerce.Domain.Entities.Product;
using ECommerce.Domain.IRepositories.Common.UnitOfWork;
using Mapster;
using MapsterMapper;
using MediatR;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(IUnitOfWorkTransactionHandler handler) : IRequestHandler<CreateCategoryCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        => await handler.ExecuteAsync(async (uow, token) =>
        {
            var category = request.Adapt<Category>();

            await uow.CategoryRepository.AddAsync(category, cancellationToken);

            var saveResult = await uow.SaveAsync(token);
            if (saveResult.IsFailure)
            {
                var error = new Error(
                    "Operation failed.",
                    "An unexpected error occurred while creating the category.",
                    ErrorType.Unexpected
                );
                return Result<long>.Failure(error);
            }
            return Result<long>.Success(category.Id);
        }, cancellationToken);
}
