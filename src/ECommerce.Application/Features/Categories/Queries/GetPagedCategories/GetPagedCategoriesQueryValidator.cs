using ECommerce.Application.Common.Validators;
using ECommerce.Domain.Entities.Product;
using FluentValidation;

namespace ECommerce.Application.Features.Categories.Queries.GetPagedCategories;

public class GetPagedCategoriesQueryValidator : QueryRequestValidator<GetPagedCategoriesQuery, Category>
{
    public GetPagedCategoriesQueryValidator() { }
}
