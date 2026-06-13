using System.Linq.Expressions;
using ECommerce.Domain.Common.Enums;

namespace ECommerce.Infrastructure.Common.Extensions;

/// <summary>
/// Provides extension methods for applying sorting to <see cref="IQueryable{T}"/> collections.
/// These extensions are used to dynamically sort queryable data based on property names and sort directions.
/// </summary>
public static class QueryableSortingExtensions
{
    /// <summary>
    /// Applies sorting to the query based on the specified property name and sort direction.
    /// </summary>
    /// <typeparam name="T">The type of entities in the queryable collection.</typeparam>
    /// <param name="query">The queryable collection to apply sorting to.</param>
    /// <param name="sortBy">The name of the property to sort by. If null or whitespace, the original query is returned unchanged.</param>
    /// <param name="sort">The direction to sort by. Defaults to ascending if not specified.</param>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> with the sorting applied, or the original query if sorting cannot be applied.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method uses reflection to dynamically create sorting expressions at runtime. 
    /// The property name is matched case-insensitively against the properties of type <typeparamref name="T"/>.
    /// </para>
    /// <para>
    /// If the specified property does not exist on type <typeparamref name="T"/>, the method returns 
    /// the original query without applying any sorting.
    /// </para>
    /// <para>
    /// This method is compatible with Entity Framework and other LINQ providers that support 
    /// expression-based sorting.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// // Apply ascending sort by "Name" property
    /// var sortedQuery = query.ApplySorting("Name", SortType.Asc);
    /// 
    /// // Apply descending sort by "CreatedDate" property  
    /// var descendingQuery = query.ApplySorting("CreatedDate", SortType.Desc);
    /// 
    /// // Invalid property name - returns original query
    /// var unchangedQuery = query.ApplySorting("NonExistentProperty", SortType.Asc);
    /// </code>
    /// </example>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="query"/> is null.</exception>
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortBy, SortType sort)
    {
        if (query is null)
            throw new ArgumentNullException(nameof(query));
        
        // Return original query if no sort property specified
        if (string.IsNullOrWhiteSpace(sortBy))
            return query;
        
        // Find the property to sort by (case-insensitive)
        var prop = typeof(T).GetProperty(sortBy, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (prop is null) return query;

        // Create lambda expression: x => x.Property
        var param = Expression.Parameter(typeof(T), "x");
        var propAccess = Expression.Property(param, prop);
        var lambda = Expression.Lambda(propAccess, param);

        // Determine the appropriate LINQ method name
        var methodName = sort is SortType.Desc ? "OrderByDescending" : "OrderBy";

        // Get the generic method and invoke it
        var method = typeof(Queryable)
            .GetMethods()
            .Single(m => m.Name == methodName && m.GetParameters().Length is 2)
            .MakeGenericMethod(typeof(T), prop.PropertyType);
        
        return (IQueryable<T>)method.Invoke(null, [query, lambda])!;
    }
}