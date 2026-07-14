namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
///     Provides URL-friendly slug generation services for entities.
/// </summary>
/// <remarks>
///     <para>
///         This service generates SEO-friendly, human-readable slugs (URL-safe strings)
///         from entity names or titles. It ensures slugs are:
///     </para>
///     <para>
///         <b>Slug Format:</b> "lowercase-words-with-hyphens"
///     </para>
/// </remarks>
public interface ISlugService
{
    /// <summary>
    ///     Generates a unique URL-friendly slug for a product based on its English name.
    /// </summary>
    /// <param name="englishName">
    ///     The English name of the product used as the base for generating the slug.
    ///     Should not be <c>null</c> or empty.
    /// </param>
    /// <param name="productId">
    ///     The optional ID of the product being updated. When provided, the service will
    ///     exclude this product from uniqueness checks, allowing the same slug to be
    ///     used for the same product during updates.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token to cancel the operation if needed.
    /// </param>
    /// <returns>
    ///     A unique, URL-safe slug string based on the product name.
    ///     Example: "wireless-bluetooth-headphones" or "wireless-bluetooth-headphones-1"
    /// </returns>
    Task<string> GenerateProductSlugAsync(
        string englishName,
        long? productId = null,
        CancellationToken cancellationToken = default);
}