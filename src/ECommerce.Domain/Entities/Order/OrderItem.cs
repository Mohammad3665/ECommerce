using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Base;

namespace ECommerce.Domain.Entities.Order;

/// <summary>
///     Represents an individual line item within a customer order.
/// </summary>
public class OrderItem : BaseEntity<Guid>
{
    #region Foreign Keys

    /// <summary>
    ///     Gets or sets the unique identifier of the order this item belongs to.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Order.Id"/>.
    /// </value>
    [ForeignKey(nameof(Order))]
    public long OrderId { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier of the product being purchased.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Product.Id"/>.
    /// </value>
    [ForeignKey(nameof(Product))]
    public long ProductId { get; set; }

    #endregion

    #region Snapshot Properties

    /// <summary>
    ///     Gets or sets the name of the product at the time of purchase.
    /// </summary>
    [Required]
    [MaxLength(150)]
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the price per unit at the time of purchase.
    /// </summary>
    /// <value>
    ///     A decimal value representing the snapshot of the product's selling price.
    /// </value>
    [Required]
    public decimal UnitPrice { get; set; }

    /// <summary>
    ///     Gets or sets the quantity of this product purchased.
    /// </summary>
    /// <value>
    ///     An integer representing how many units of the product were ordered.
    /// </value>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    ///     Gets or sets the total price for this line item (UnitPrice × Quantity).
    /// </summary>
    /// <value>
    ///     A decimal value representing the subtotal for this specific item before order-level discounts.
    /// </value>
    [Required]
    public decimal TotalPrice { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the parent order that contains this item.
    /// </summary>
    /// <value>
    ///     An <see cref="Order"/> entity representing the order to which this item belongs.
    /// </value>
    public Order Order { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the product associated with this order item.
    /// </summary>
    /// <value>
    ///     A <see cref="Product"/> entity representing the purchased product.
    /// </value>
    public Product.Product? Product { get; set; }

    #endregion
}