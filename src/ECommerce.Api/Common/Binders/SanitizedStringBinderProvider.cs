using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ECommerce.Api.Common.Binders;

/// <summary>
///     Provides the <see cref="SanitizedStringBinder"/> for string model binding.
/// </summary>
/// <remarks>
///     <para>
///         This provider is responsible for injecting the HTML sanitization binder
///         into the MVC model binding pipeline for all string properties and parameters.
///     </para>
/// </remarks>
public class SanitizedStringBinderProvider(IHtmlSanitizerService sanitizer) : IModelBinderProvider
{
    /// <summary>
    ///     Returns a <see cref="SanitizedStringBinder"/> for string types, or <c>null</c> for other types.
    /// </summary>
    /// <param name="context">
    ///     The <see cref="ModelBinderProviderContext"/> containing metadata about the model to bind.
    /// </param>
    /// <returns>
    ///     A <see cref="SanitizedStringBinder"/> instance if the model type is <see cref="string"/>;
    ///     otherwise, <c>null</c> to fall back to the default model binder.
    /// </returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(string))
            return new SanitizedStringBinder(sanitizer);

        return null!;
    }
}