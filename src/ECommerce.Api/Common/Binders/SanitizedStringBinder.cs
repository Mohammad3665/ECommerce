using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Api.Common.Binders;

/// <summary>
///     Model binder that automatically sanitizes string values to prevent XSS attacks.
/// </summary>
/// <remarks>
///     <para>
///         This binder intercepts string parameters and model properties, applying HTML sanitization
///         to remove potentially dangerous content before it reaches the application logic.
///     </para>
/// </remarks>
public class SanitizedStringBinder(IHtmlSanitizerService sanitizer) : IModelBinder
{
    /// <summary>
    ///     Binds the model by sanitizing the incoming string value.
    /// </summary>
    /// <param name="bindingContext">
    ///     The <see cref="ModelBindingContext"/> containing the value to bind.
    /// </param>
    /// <returns>
    ///     A completed task with the sanitized string as the binding result.
    /// </returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var value = bindingContext.ValueProvider.GetValue(bindingContext.FieldName).FirstValue;
        var clean = sanitizer.Clean(value);
        bindingContext.Result = ModelBindingResult.Success(clean);
        return Task.CompletedTask;
    }
}