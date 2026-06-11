using System.ComponentModel.DataAnnotations;
using ECommerce.Domain.Entities.Base;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Order;

/// <summary>
///     Represents a promotional coupon or discount code in the e-commerce system.
/// </summary>
public class Coupon : BaseEntity<Guid>
{
    #region Basic Information

    /// <summary>
    ///     Gets or sets the unique discount code that customers enter at checkout.
    /// </summary>
    /// <value>
    ///     A string containing the promotional code. Must be unique across the system.
    /// </value>
    [Required]
    [MaxLength(30)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the type of discount this coupon provides.
    /// </summary>
    /// <value>
    ///     A <see cref="CouponType"/> enum value (Percentage or FixedAmount).
    /// </value>
    public CouponType Type { get; set; }

    /// <summary>
    ///     Gets or sets the discount amount or percentage.
    /// </summary>
    /// <value>
    ///     A decimal value representing either:
    ///     - Percentage (0-100) when <see cref="Type"/> is <see cref="CouponType.Percentage"/>
    ///     - Fixed amount in store currency when <see cref="Type"/> is <see cref="CouponType.FixedAmount"/>
    /// </value>
    [Required]
    public decimal Value { get; set; }

    #endregion

    #region Eligibility Rules

    /// <summary>
    ///     Gets or sets the minimum order amount required to use this coupon.
    /// </summary>
    /// <value>
    ///     A nullable decimal representing the minimum subtotal before discount.
    ///     <c>null</c> indicates no minimum requirement.
    /// </value>
    public decimal? MinOrderAmount { get; set; }

    /// <summary>
    ///     Gets or sets the maximum number of times this coupon can be used across all customers.
    /// </summary>
    /// <value>
    ///     A nullable integer representing total usage limit.
    ///     <c>null</c> indicates unlimited usage.
    /// </value>
    public int? UsageLimit { get; set; }

    /// <summary>
    ///     Gets or sets the number of times this coupon has been used successfully.
    /// </summary>
    /// <value>
    ///     An integer counter tracking total successful coupon applications.
    /// </value>
    [Required]
    public int UsedCount { get; set; }

    #endregion

    #region Validity Period

    /// <summary>
    ///     Gets or sets the date and time when the coupon becomes valid.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime"/> value representing the activation timestamp.
    /// </value>
    [Required]
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     Gets or sets the date and time when the coupon expires.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime"/> value representing the expiration timestamp.
    /// </value>
    [Required]
    public DateTime EndDate { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the coupon is currently active.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the coupon is enabled for use; otherwise, <c>false</c>. Default is <c>true</c>.
    /// </value>
    public bool IsActive { get; set; } = true;

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the collection of orders that have used this coupon.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="Order"/> entities that applied this discount code.
    /// </value>
    public ICollection<Order> Orders { get; set; } = [];

    #endregion
}