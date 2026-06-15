namespace ECommerce.Domain.Entities.Product;

/// <summary>
///     Represents a product brand or manufacturer in the catalog system.
/// </summary>
public class Brand : BaseEntity<long>
{
    #region Properties

    /// <summary>
        ///     Gets or sets the display name of the brand.
        /// </summary>
        /// <value>
        ///     A string containing the brand's full name (e.g., "Nike", "Apple"). Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the english name of the brand.
    /// </summary>
    /// <value>
    ///     A string containing the brand's english name (e.g., "Nike", "Apple"). Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(100)]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the URL-friendly unique identifier for the brand.
        /// </summary>
        /// <value>
        ///     A SEO-friendly string derived from <see cref="Name"/> (e.g., "nike", "apple-iphone"). Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(1000)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the detailed description of the brand.
        /// </summary>
        /// <value>
        ///     A string containing brand information, history, or any relevant details. Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(1500)]
    public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the URL of the brand's logo image.
        /// </summary>
        /// <value>
        ///     A string representing the relative or absolute path to the logo image. Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(300)]
    public string LogoImageUrl { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets a value indicating whether the brand is visible and active in the store.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the brand is displayed on the website and available for product association;
        ///     otherwise, <c>false</c>. Default is <c>true</c>.
        /// </value>
    public bool IsActive { get; set; } = true;

    #endregion

    #region Relations

    /// <summary>
        ///     Gets or sets the collection of products belonging to this brand.
        /// </summary>
        /// <value>
        ///     A collection of <see cref="Product"/> entities associated with this brand. Defaults to an empty list.
        /// </value>
    public ICollection<Product> Products { get; set; } = [];

    #endregion
}