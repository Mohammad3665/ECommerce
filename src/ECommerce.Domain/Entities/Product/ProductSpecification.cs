namespace ECommerce.Domain.Entities.Product;

/// <summary>
///     Represents a technical specification or attribute key-value pair for a product.
/// </summary>
public class ProductSpecification : BaseEntity<long>
{
    #region Properties

    /// <summary>
    ///     Gets or sets the name or title of the specification attribute.
    /// </summary>
    /// <value>
    ///     A string representing the specification type (e.g., "Color", "Storage", "Processor", "RAM", "Weight").
    /// </value>
    [Required]
    [MaxLength(50)]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the actual value of the specification attribute.
    /// </summary>
    /// <value>
    ///     A string representing the specification detail (e.g., "Red", "256GB", "Intel i7", "2.5 kg").
    /// </value>
    [Required]
    [MaxLength(50)]
    public string Value { get; set; } = string.Empty;

    #endregion

    #region Foreign Key

    /// <summary>
    ///     Gets or sets the unique identifier of the product this specification belongs to.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Product.Id"/>.
    /// </value>
    [ForeignKey(nameof(Product))]
    public long ProductId { get; set; }

    #endregion
    
    #region Realtions
    
    /// <summary>
    ///     Gets or sets the product associated with this specification.
    /// </summary>
    /// <value>
    ///     A <see cref="Product"/> entity that owns this specification.
    /// </value>
    public Product Product { get; set; } = null!;

    #endregion
}