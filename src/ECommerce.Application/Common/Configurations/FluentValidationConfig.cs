using System.Globalization;
using FluentValidation;
using FluentValidation.Resources;

namespace ECommerce.Application.Common.Configurations;

/// <summary>
/// Configures FluentValidation to use Persian (Farsi) messages globally.
/// This class overrides the default LanguageManager with a custom one.
/// </summary>
public static class FluentValidationConfig
{
    /// <summary>
    /// Applies global configuration for FluentValidation error messages in Persian.
    /// Call this method once during application startup (e.g., Program.cs).
    /// </summary>
    public static void Configure()
    {
        // Set a custom LanguageManager for Persian messages
        ValidatorOptions.Global.LanguageManager = new PersianLanguageManager();
    }
}


/// <summary>
/// Custom LanguageManager for FluentValidation to provide Persian error messages.
/// </summary>
public class PersianLanguageManager : LanguageManager
{
    /// <summary>
    /// Initializes the FarsiLanguageManager with translations for standard validators.
    /// </summary>
    public PersianLanguageManager()
    {
        Culture = new CultureInfo("fa");

        // Basic validators
        // NotEmptyValidator: Checks that the property is not empty or null.
        AddTranslation("fa", "NotEmptyValidator", "فیلد '{PropertyName}' نمی‌تواند خالی باشد.");

        // NotNullValidator: Checks that the property is not null.
        AddTranslation("fa", "NotNullValidator", "فیلد '{PropertyName}' نمی‌تواند null باشد.");

        // NullValidator: Checks that the property must be null.
        AddTranslation("fa", "NullValidator", "فیلد '{PropertyName}' باید null باشد.");

        // MaximumLengthValidator: Checks that the string property does not exceed a maximum length.
        AddTranslation("fa", "MaximumLengthValidator", "فیلد '{PropertyName}' نمی‌تواند بیشتر از {MaxLength} کاراکتر باشد.");

        // MinimumLengthValidator: Checks that the string property is at least a minimum length.
        AddTranslation("fa", "MinimumLengthValidator", "فیلد '{PropertyName}' نمی‌تواند کمتر از {MinLength} کاراکتر باشد.");

        // LengthValidator: Checks that the string property length is between min and max.
        AddTranslation("fa", "LengthValidator", "طول فیلد '{PropertyName}' باید بین {MinLength} و {MaxLength} کاراکتر باشد.");

        // InclusiveBetweenValidator: Checks that the value is within a range inclusive.
        AddTranslation("fa", "InclusiveBetweenValidator", "فیلد '{PropertyName}' باید بین {From} و {To} باشد.");

        // ExclusiveBetweenValidator: Checks that the value is within a range exclusive.
        AddTranslation("fa", "ExclusiveBetweenValidator", "فیلد '{PropertyName}' باید بزرگ‌تر از {From} و کمتر از {To} باشد.");

        // EqualValidator: Checks that the property equals a comparison value.
        AddTranslation("fa", "EqualValidator", "فیلد '{PropertyName}' باید برابر با '{ComparisonValue}' باشد.");

        // NotEqualValidator: Checks that the property does not equal a comparison value.
        AddTranslation("fa", "NotEqualValidator", "فیلد '{PropertyName}' نمی‌تواند برابر با '{ComparisonValue}' باشد.");

        // EmailValidator: Checks that the property is a valid email address.
        AddTranslation("fa", "EmailValidator", "فیلد '{PropertyName}' باید یک ایمیل معتبر باشد.");

        // EnumValidator: Checks that the value is a defined enum value.
        AddTranslation("fa", "EnumValidator", "فیلد '{PropertyName}' مقدار نامعتبر دارد و باید یکی از مقادیر معتبر باشد.");

        // LengthRangeValidator: Checks that the string length falls within a range.
        AddTranslation("fa", "LengthRangeValidator", "طول فیلد '{PropertyName}' باید بین {MinLength} و {MaxLength} کاراکتر باشد.");

        // GreaterThanValidator: Checks that the value is greater than a comparison value.
        AddTranslation("fa", "GreaterThanValidator", "فیلد '{PropertyName}' باید بزرگ‌تر از {ComparisonValue} باشد.");

        // GreaterThanOrEqualValidator: Checks that the value is greater than or equal to a comparison value.
        AddTranslation("fa", "GreaterThanOrEqualValidator", "فیلد '{PropertyName}' باید بزرگ‌تر یا مساوی با {ComparisonValue} باشد.");

        // LessThanValidator: Checks that the value is less than a comparison value.
        AddTranslation("fa", "LessThanValidator", "فیلد '{PropertyName}' باید کمتر از {ComparisonValue} باشد.");

        // LessThanOrEqualValidator: Checks that the value is less than or equal to a comparison value.
        AddTranslation("fa", "LessThanOrEqualValidator", "فیلد '{PropertyName}' باید کمتر یا مساوی با {ComparisonValue} باشد.");
    }
}