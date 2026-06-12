using System.Text.Json.Serialization;
using ECommerce.Domain.Common.Enums;

namespace ECommerce.Domain.Common.Filter;

#region Query Request
/// <summary>
/// Represents a query request for filtered, sorted, and paginated data.
/// Used as the base class for all data retrieval operations that require filtering and pagination.
/// </summary>
public class QueryRequest : IValidatableObject
{
    /// <summary>
    /// The page number to retrieve. Page numbers start from 1.
    /// Default value is 1.
    /// </summary>
    /// <example>1</example>
    [Range(1, int.MaxValue, ErrorMessage = "Page must be at least 1")]
    public int Page { get; set; } = 1;

    /// <summary>
    /// The number of records to return per page.
    /// Must be between 1 and 100. Default value is 10.
    /// </summary>
    /// <example>10</example>
    [Range(1, 100, ErrorMessage = "Take must be between 1 and 100")]
    public int Take { get; set; } = 10;

    /// <summary>
    /// The property name to sort by. Must match a property name on the target entity.
    /// If null or empty, default sorting will be applied.
    /// </summary>
    /// <example>Name</example>
    [StringLength(50, ErrorMessage = "SortBy must not exceed 50 characters")]
    public string? SortBy { get; set; }

    /// <summary>
    /// The sort direction for the results.
    /// Default value is Ascending.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SortType Sort { get; set; } = SortType.Asc;
    
    /// <summary>
    /// Collection of filter conditions to apply to the query.
    /// Multiple filters are combined using AND logic.
    /// </summary>
    public List<FilterCondition>? Filters { get; set; } = [];
    
    /// <summary>
    /// Collection of property names to group results by.
    /// If null, no grouping is applied.
    /// </summary>
    public List<string>? GroupBy { get; set; } = null;
    
    /// <summary>
    /// Validates the QueryRequest instance and all contained FilterCondition objects.
    /// </summary>
    /// <param name="validationContext">The validation context.</param>
    /// <returns>A collection of validation results.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Filters != null)
        {
            foreach (var filter in Filters)
            {
                var filterResults = new List<ValidationResult>();
                var filterContext = new ValidationContext(filter);
                if (!Validator.TryValidateObject(filter, filterContext, filterResults, true))
                {
                    foreach (var error in filterResults)
                    {
                        yield return error;
                    }
                }
            }
        }
    }
}
#endregion

#region Filter Condition
/// <summary>
/// Represents a single filter condition to apply to a query.
/// Each condition specifies a property, operator, and value to filter by.
/// </summary>
public class FilterCondition : IValidatableObject
{
    /// <summary>
    /// The name of the property to filter on. 
    /// Supports dot notation for nested properties (e.g., "Address.City").
    /// Must be a valid property path containing only alphanumeric characters and underscores.
    /// </summary>
    /// <example>Name</example>
    /// <example>Address.City</example>
    [Required(ErrorMessage = "Property is Required")]
    [StringLength(100, ErrorMessage = "Property must not exceed 100 characters")]
    [RegularExpression(@"^[a-zA-Z_][a-zA-Z0-9_]*(\.[a-zA-Z_][a-zA-Z0-9_]*)*$", 
    ErrorMessage = "Prperty must be a valid property path")]
    public string Property { get; set; } = string.Empty;

    /// <summary>
    /// The operator to use for the filter comparison.
    /// Default value is Equals.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterOperator Operator { get; set; } = FilterOperator.Equals;

    /// <summary>
    /// The value to compare against using the specified operator.
    /// Required for all operators except In (which can accept empty values).
    /// For In operator, provide comma-separated values.
    /// </summary>
    /// <example>John</example>
    /// <example>100</example>
    /// <example>value1,value2,value3</example>
    public string? Value { get; set; }

    /// <summary>
    /// Validates the FilterCondition instance.
    /// Ensures Value is provided for operators that require it.
    /// </summary>
    /// <param name="validationContext">The validation context.</param>
    /// <returns>A collection of validation results.</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Operator != FilterOperator.In && Value == null)
        {
            yield return new ValidationResult("Value is required for this operator", [nameof(Value)]);
        }
    }
}
#endregion