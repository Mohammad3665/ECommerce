using System.ComponentModel;
using System.Globalization;

namespace ECommerce.Infrastructure.Common.Helpers;

/// <summary>
/// Provides utility methods for converting string values to various types with proper null handling and error tolerance.
/// This helper is primarily used by query filtering systems to convert filter values from string to target property types.
/// </summary>
public class TypeConversionHelper
{
    /// <summary>
    /// Attempts to convert a string value to the specified target type with comprehensive type support.
    /// Supports strings, enums, GUIDs, nullable types, and all types with TypeConverter support.
    /// </summary>
    /// <param name="value">The string value to convert. Can be null.</param>
    /// <param name="targetType">The target type to convert to.</param>
    /// <param name="converted">When this method returns, contains the converted value if successful; otherwise, null.</param>
    /// <returns>
    /// true if the conversion was successful; false if the conversion failed or the value is null for non-nullable value types.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method handles the following type categories:
    /// </para>
    /// <list type="bullet">
    /// <item><description>String values (direct assignment)</description></item>
    /// <item><description>Enum values (case-insensitive parsing)</description></item>
    /// <item><description>GUID values (standard GUID parsing)</description></item>
    /// <item><description>Nullable types (proper null handling)</description></item>
    /// <item><description>Types with TypeConverter support</description></item>
    /// <item><description>Fallback to Convert.ChangeType for other primitive types</description></item>
    /// </list>
    /// <para>
    /// Conversion failures are silently caught and return false rather than throwing exceptions,
    /// making this method suitable for filter scenarios where invalid values should be ignored.
    /// </para>
    /// </remarks>
    public static bool TryConvert(string? value, Type targetType, out object? converted)
    {
        converted = null;

        // Handle null values
        if (value is null)
        {
            if (IsNullableType(targetType))
            {
                converted = null;
                return true;
            }
            return false;
        }

        var nonNullable = Nullable.GetUnderlyingType(targetType) ?? targetType;

        try
        {
            // Handle string types
            if (nonNullable == typeof(string))
            {
                converted = value;
                return true;
            }

            // Handle enum types
            if (nonNullable.IsEnum)
            {
                if (Enum.TryParse(nonNullable, value, true, out var enumValue))
                {
                    converted = enumValue;
                    return true;
                }
                return false;
            }

            // Handle Guid types
            if (nonNullable == typeof(Guid))
            {
                if (Guid.TryParse(value, out var g))
                {
                    converted = g;
                    return true;
                }
                return false;
            }

            // Use TypeConverter for types that support it
            var converter = TypeDescriptor.GetConverter(nonNullable);
            if (converter.IsValid(value))
            {
                converted = converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
                return true;
            }
            
            // Final fallback for primitive types
            converted = Convert.ChangeType(value, nonNullable, CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Determines whether the specified type is a nullable type (either reference type or Nullable{T}).
    /// </summary>
    /// <param name="t">The type to check.</param>
    /// <returns>
    /// true if the type is a reference type or Nullable{T}; otherwise, false.
    /// </returns>
    private static bool IsNullableType(Type t) => Nullable.GetUnderlyingType(t) != null || !t.IsValueType;
}