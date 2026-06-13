using System.Linq.Expressions;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Common.Enums;
using System.Reflection;
using ECommerce.Infrastructure.Common.Helpers;
namespace ECommerce.Infrastructure.Common.Extensions;

/// <summary>
/// Provides extension methods for applying filter conditions to <see cref="IQueryable{T}"/> collections.
/// </summary>
public static class QueryableFilterExtensions
{
    /// <summary>
    /// Applies a collection of filter conditions to the query.
    /// </summary>
    /// <typeparam name="T">The type of entities in the queryable collection.</typeparam>
    /// <param name="query">The queryable collection to apply filters to.</param>
    /// <param name="filters">The collection of filter conditions to apply.</param>
    /// <returns>An <see cref="IQueryable{T}"/> with the filter conditions applied.</returns>
    public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, IEnumerable<FilterCondition>? filters)
    {
        if (filters is null) return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? combined = null;

        foreach (var filter in filters)
        {
            if (string.IsNullOrWhiteSpace(filter.Property)) continue;

            // find property (support top-level only; for navigation you'd parse dotted path)
            var prop = typeof(T).GetProperty(filter.Property, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop is null) continue;

            var member = Expression.Property(parameter, prop);
            var targetType = prop.PropertyType;
            var nonNullableType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            // For string ops we'll need a string expression
            Expression? predicate = null;

            switch (filter.Operator)
            {
                case FilterOperator.Equals:
                case FilterOperator.NotEquals:
                case FilterOperator.GreaterThan:
                case FilterOperator.GreaterThanOrEqual:
                case FilterOperator.LessThan:
                case FilterOperator.LessThanOrEqual:
                {
                    if (!TypeConversionHelper.TryConvert(filter.Value, targetType, out var converted)) continue;

                    var constant = Expression.Constant(converted, targetType);
                    predicate = filter.Operator switch
                    {
                        FilterOperator.Equals => Expression.Equal(member, constant),
                        FilterOperator.NotEquals => Expression.NotEqual(member, constant),
                        FilterOperator.GreaterThan => Expression.GreaterThan(member, constant),
                        FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
                        FilterOperator.LessThan => Expression.LessThan(member, constant),
                        FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
                        _ => null
                    };

                }
                break;

                case FilterOperator.Contains:
                case FilterOperator.StartsWith:
                case FilterOperator.EndsWith:
                {
                    // Build string call: member?.ToString()?.Contains(value)
                    Expression memberAsString = prop.PropertyType == typeof(string) 
                    ? (Expression)member 
                    : Expression.Call(member, prop.PropertyType.GetMethod("ToString", Type.EmptyTypes)!);

                    if (filter.Value is null) continue;
                    var methodName = filter.Operator is FilterOperator.Contains ? "Contains"
                        : filter.Operator is FilterOperator.StartsWith ? "StartsWith"
                        : "EndsWith";
                    var method = typeof(string).GetMethod(methodName, new[] { typeof(string) })!;
                    var constant = Expression.Constant(filter.Value, typeof(string));

                    predicate = Expression.Call(memberAsString, method, constant);
                }
                break;

                case FilterOperator.In:
                {
                    if (string.IsNullOrEmpty(filter.Value)) continue;
                    var parts = filter.Value.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToArray();
                    if (parts.Length is 0) continue;

                    // Build OR of equality comparisons (safer & EF-translatable)
                    Expression? orExpr = null;
                    foreach (var part in parts)
                    {
                        if (!TypeConversionHelper.TryConvert(part, targetType, out var conv)) continue;
                        var constant = Expression.Constant(conv, targetType);
                        var eq = Expression.Equal(member, constant);
                        orExpr = orExpr is null ? eq : Expression.OrElse(orExpr, eq);
                    }
                    predicate = orExpr;
                }
                break;

            }
            
            if (predicate is null) continue;
            combined = combined is null ? predicate : Expression.AndAlso(combined, predicate);

        }
        if (combined is null) return query;
        var lambda = Expression.Lambda<Func<T, bool>>(combined, parameter);
        return query.Where(lambda);
    }
}