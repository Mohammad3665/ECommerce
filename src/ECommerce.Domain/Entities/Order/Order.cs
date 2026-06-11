using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Base;
using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Order;

/// <summary>
///     Represents a customer purchase order in the e-commerce system.
/// </summary>
public class Order : BaseEntity<long>
{
    #region Basic Information

    /// <summary>
    ///     Gets or sets the unique human-readable identifier for the order.
    /// </summary>
    /// <value>
    ///     A string containing the order reference number. Must be unique across the system.
    /// </value>
    [Required]
    [MaxLength(20)]
    public string OrderNumber { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the unique identifier of the customer who placed the order.
    /// </summary>
    /// <value>
    ///     A <see cref="Guid"/> value referencing <see cref="User.Id"/>.
    /// </value>
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp when the order was created.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime"/> value representing the exact moment of order placement.
    /// </value>
    [Required]
    public DateTime OrderDate { get; set; }

    /// <summary>
    ///     Gets or sets the current state of the order in the fulfillment workflow.
    /// </summary>
    /// <value>
    ///     An <see cref="OrderStatus"/> enum value representing the order's lifecycle stage.
    /// </value>
    public OrderStatus Status { get; set; }

    #endregion

    #region Pricing Breakdown

    /// <summary>
    ///     Gets or sets the total price of all items before any discounts or shipping.
    /// </summary>
    /// <value>
    ///     A decimal value representing the sum of (item price × quantity) for all order items.
    /// </value>
    [Required]
    public decimal SubTotal { get; set; }

    /// <summary>
    ///     Gets or sets the total discount applied to the order.
    /// </summary>
    /// <value>
    ///     A decimal value representing the reduction in price from coupons, promotions, or manual adjustments.
    /// </value>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    ///     Gets or sets the cost of shipping the order to the customer.
    /// </summary>
    /// <value>
    ///     A decimal value representing delivery fees based on shipping method, weight, or distance.
    /// </value>
    [Required]
    public decimal ShippingCost { get; set; }

    /// <summary>
    ///     Gets or sets the final amount the customer needs to pay.
    /// </summary>
    /// <value>
    ///     A decimal value representing the grand total after all calculations.
    /// </value>
    [Required]
    public decimal TotalAmount { get; set; }

    #endregion

    #region Relations
    
    /// <summary>
    ///     Gets or sets the customer who placed this order.
    /// </summary>
    /// <value>
    ///     A <see cref="User"/> entity representing the order owner.
    /// </value>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the collection of products/items included in this order.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="OrderItem"/> entities representing line items. Defaults to an empty list.
    /// </value>
    public ICollection<OrderItem> Items { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of status change records for this order.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="OrderHistory"/> entities for audit trail. Defaults to an empty list.
    /// </value>
    public ICollection<OrderHistory> Histories { get; set; } = [];

    /// <summary>
    ///     Gets or sets the coupon applied to this order.
    /// </summary>
    /// <value>
    ///     A <see cref="Coupon"/> entity, or <c>null</c> if no coupon was used.
    /// </value>
    public Coupon? Coupon { get; set; }

    #endregion
}
