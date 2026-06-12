using System.Text.Json.Serialization;

namespace ECommerce.Domain.Common.Enums;

/// <summary>
/// Defines the available comparison operators for filter conditions.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterOperator
{
    /// <summary>
    /// Equal comparison.
    /// </summary>
    Equals,

    /// <summary>
    /// Not equal comparison.
    /// </summary>
    NotEquals,

    /// <summary>
    /// Contains the specified value (string only).
    /// </summary>
    Contains,

    /// <summary>
    /// Starts with the specified value (string only).
    /// </summary>
    StartsWith,

    /// <summary>
    /// Ends with the specified value (string only).
    /// </summary>
    EndsWith,

    /// <summary>
    /// Greater than comparison.
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Greater than or equal comparison.
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// Less than comparison.
    /// </summary>
    LessThan,

    /// <summary>
    /// Less than or equal comparison.
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// Value is contained in a list of comma-separated values.
    /// </summary>
    In
}