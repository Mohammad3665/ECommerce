using ECommerce.Domain.Common.Filter;

namespace ECommerce.Application.Common.Validators;

/// <summary>
/// Validates query request parameters for pagination, sorting, and filtering.
/// </summary>
/// <typeparam name="TQuery">The query request type inheriting from QueryRequest.</typeparam>
/// <typeparam name="TEntity">The entity type being queried for property validation.</typeparam>
public class QueryRequestValidator<TQuery, TEntity> : AbstractValidator<TQuery> where TQuery : QueryRequest
{
    public QueryRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1)
            .WithName("شماره صفحه");

        RuleFor(x => x.Take)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithName("تعداد رکوردها");

        RuleFor(x => x.SortBy)
            .Must(BeAValidProperty).WithMessage($"ستون انتخاب شده برای مرتب‌سازی در موجودیت {typeof(TEntity).Name} وجود ندارد.")
            .When(x => !string.IsNullOrWhiteSpace(x.SortBy));

        RuleForEach(x => x.Filters)
            .ChildRules(filter =>
            {
                filter.RuleFor(f => f.Property)
                    .NotEmpty().WithName("نام ستون فیلتر")
                    .Must(BeAValidProperty).WithMessage($"ستون انتخاب شده برای فیلتر در موجودیت {typeof(TEntity).Name} وجود ندارد.");

                filter.RuleFor(f => f.Operator)
                    .IsInEnum().WithMessage("عملگر فیلتر (Operator) ارسالی معتبر نیست.");

                filter.RuleFor(f => f.Value)
                    .NotEmpty().WithMessage("مقدار فیلتر (Value) نمی‌تواند خالی باشد.");
            })
            .When(x => x.Filters is not null && x.Filters.Any());

    }

    /// <summary>
    /// Checks if the given property name exists on the entity type.
    /// </summary>
    /// <param name="propertyName">The property name to validate.</param>
    /// <returns>True if the property exists; otherwise, false.</returns>
    private bool BeAValidProperty(string? propertyName)
    {
        if (string.IsNullOrWhiteSpace(propertyName)) return false;

        var prop = typeof(TEntity).GetProperty(
            propertyName,
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance
        );

        return prop is not null;
    }
}
