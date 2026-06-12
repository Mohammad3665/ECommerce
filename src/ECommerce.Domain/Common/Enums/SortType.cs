using System.Text.Json.Serialization;

namespace ECommerce.Domain.Common.Enums;

/// <summary>
/// Defines the available sort directions.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortType
{
    /// <summary>
    /// Sort in ascending order (A to Z, 0 to 9).
    /// </summary>
    Asc = 0,

    /// <summary>
    /// Sort in descending order (Z to A, 9 to 0).
    /// </summary>
    Desc = 1
}