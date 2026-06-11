namespace ECommerce.Domain.Entities.Product;

/// <summary>
///     Represents an image associated with a product in the catalog system.
/// </summary>
public class ProductImage : BaseEntity<long>
{
    #region Properties

    /// <summary>
        ///     Gets or sets the URL or file path of the product image.
        /// </summary>
        /// <value>
        ///     A string representing the relative or absolute path to the image file.
        /// </value>
    [Required]
    [MaxLength(300)]
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets a value indicating whether this is the primary image of the product.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this image is the main thumbnail; otherwise, <c>false</c>. Default is <c>false</c>.
        /// </value>
    public bool IsMain { get; set; }

    /// <summary>
        ///     Gets or sets the sorting order of the image in the product gallery.
        /// </summary>
        /// <value>
        ///     An integer representing the display sequence (lower values appear first). Default is 0.
        /// </value>
    public int DisplayOrder { get; set; }

    #endregion

    #region Foreign key

    /// <summary>
        ///     Gets or sets the unique identifier of the product this image belongs to.
        /// </summary>
        /// <value>
        ///     A <see cref="long"/> value referencing <see cref="Product.Id"/>.
        /// </value>
    [ForeignKey(nameof(Product))]
    public long ProductId { get; set; }

    #endregion

    #region  Relations

    /// <summary>
        ///     Gets or sets the product associated with this image.
        /// </summary>
        /// <value>
        ///     A <see cref="Product"/> entity that owns this image.
        /// </value>
    public Product Product { get; set; } = null!;

    #endregion
}