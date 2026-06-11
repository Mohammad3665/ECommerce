using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Base;

namespace ECommerce.Domain.Entities.Product;

/// <summary>
///     Represents a product category or hierarchical classification in the catalog system.
/// </summary>
public class Category : BaseEntity<long>
{
    #region Properties

    /// <summary>
        ///     Gets or sets the display name of the category.
        /// </summary>
        /// <value>
        ///     A string containing the category's title as shown to users. Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the URL-friendly unique identifier for the category.
        /// </summary>
        /// <value>
        ///     A SEO-friendly string derived from <see cref="Name"/> (e.g., "electronics", "gaming-laptops"). Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(1000)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the detailed description of the category.
        /// </summary>
        /// <value>
        ///     A string containing category information, SEO meta description, or user guidance. Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(1500)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the URL of the category's representative image or banner.
        /// </summary>
        /// <value>
        ///     A string representing the path to the category image. Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(300)]
    public string ImageUrl { get; set; } = string.Empty;

    #endregion

    #region Foreign Key

    /// <summary>
        ///     Gets or sets the identifier of the parent category.
        /// </summary>
        /// <value>
        ///     A nullable <see cref="long"/> representing the parent category ID.
        ///     <c>null</c> indicates this is a root-level (top-level) category.
        /// </value>
    [ForeignKey(nameof(Category))]
    public long? ParentCategoryId { get; set; }

    #endregion

    #region  Relations

    /// <summary>
        ///     Gets or sets the parent category of this category.
        /// </summary>
        /// <value>
        ///     A <see cref="Category"/> object representing the parent, or <c>null</c> for root categories.
        /// </value>
    public Category? ParentCategory { get; set; }

    /// <summary>
        ///     Gets or sets the collection of child subcategories under this category.
        /// </summary>
        /// <value>
        ///     A collection of <see cref="Category"/> entities representing direct children. Defaults to an empty list.
        /// </value>
    public ICollection<Category> SubCategpries { get; set; } = [];

    /// <summary>
        ///     Gets or sets the collection of products belonging to this category.
        /// </summary>
        /// <value>
        ///     A collection of <see cref="Product"/> entities associated with this category. Defaults to an empty list.
        /// </value>
    public ICollection<Product> Products { get; set; } = [];

    #endregion
}