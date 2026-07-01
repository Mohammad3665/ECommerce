using ECommerce.Domain.Entities.Order;

namespace ECommerce.Domain.Entities.Product;

/// <summary>
///     Represents a purchasable product in the e-commerce catalog system.
/// </summary>
public class Product : BaseEntity<long>
{
    #region Basic Information

    /// <summary>
    ///     Gets or sets the display name of the product.
    /// </summary>
    /// <value>
    ///     A string containing the product's title as shown to customers. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the english name of the product.
    /// </summary>
    /// <value>
    ///     A string containing the product's english title. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(150)]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the URL-friendly unique identifier for the product.
    /// </summary>
    /// <value>
    ///     A SEO-friendly string derived from <see cref="Name"/> (e.g., "iphone-15-pro-max"). Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(1000)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the full detailed description of the product.
    /// </summary>
    /// <value>
    ///     A string containing comprehensive product information, features, and specifications. Defaults to empty string.
    /// </value>
    [Required]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the brief summary of the product.
    /// </summary>
    /// <value>
    ///     A short string (typically 150-200 characters) summarizing the product. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(300)]
    public string ShortDescription { get; set; } = string.Empty;

    #endregion

    #region Pricing And Inventory

    /// <summary>
    ///     Gets or sets the current selling price of the product.
    /// </summary>
    /// <value>
    ///     A decimal value representing the price in the store's default currency.
    /// </value>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    ///     Gets or sets the available quantity of the product in inventory.
    /// </summary>
    /// <value>
    ///     An integer representing how many units are in stock. Defaults to 0.
    /// </value>
    [Required]
    public int StockQuantity { get; set; }

    /// <summary>
    ///     Gets or sets the number of times the product has been viewed by users.
    /// </summary>
    /// <value>
    ///     An integer counter tracking product popularity. Defaults to 0.
    /// </value>
    public int ViewCount { get; set; }

    /// <summary>
    ///     Gets a value indicating whether the product is in stock.
    /// </summary>
    /// <value>
    ///     <c>true</c> if StockQuantity is greater than zero; otherwise, <c>false</c>.
    /// </value>
    public bool IsInStock => StockQuantity > 0;

    #endregion

    #region Foreign keys

    /// <summary>
    ///     Gets or sets the unique identifier of the brand this product belongs to.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Brand.Id"/>.
    /// </value>
    [ForeignKey(nameof(Brand))]
    public long BrandId { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier of the category this product belongs to.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Category.Id"/>.
    /// </value>
    [ForeignKey(nameof(Category))]
    public long CategoryId { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the brand associated with this product.
    /// </summary>
    /// <value>
    ///     A <see cref="Brand"/> entity containing brand information.
    /// </value>
    public Brand Brand { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the category associated with this product.
    /// </summary>
    /// <value>
    ///     A <see cref="Category"/> entity containing category information.
    /// </value>
    public Category Category { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the collection of images associated with this product.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="ProductImage"/> entities for the product gallery. Defaults to an empty list.
    /// </value>
    public ICollection<ProductImage> Images { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of technical specifications for this product.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="ProductSpecification"/> entities (key-value pairs like "RAM: 8GB"). Defaults to an empty list.
    /// </value>
    public ICollection<ProductSpecification> Specifications { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of user comments and reviews for this product.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="Comment"/> entities. Defaults to an empty list.
    /// </value>
    public ICollection<Comment> Comments { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of order items linking this product to customer orders.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="OrderItem"/> entities. Defaults to an empty list.
    /// </value>
    public ICollection<OrderItem> OrderItems { get; set; } = [];

    #endregion
}