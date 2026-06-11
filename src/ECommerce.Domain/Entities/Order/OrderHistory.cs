using ECommerce.Domain.Entities.Identity;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Order;

/// <summary>
///     Represents an audit trail entry for order status changes throughout the order lifecycle.
/// </summary>
public class OrderHistory : BaseEntity<long>
{
    #region Foreign Keys

    /// <summary>
    ///     Gets or sets the unique identifier of the order this history entry belongs to.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Order.Id"/>.
    /// </value>
    [ForeignKey(nameof(Order))]
    public long OrderId { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the user who performed the status change.
    /// </summary>
    /// <value>
    ///     A nullable <see cref="Guid"/> referencing <see cref="User.Id"/>.
    ///     <c>null</c> indicates the change was performed by the system (automated process).
    /// </value>
    [ForeignKey(nameof(User))]
    public Guid? ChangedByUserId { get; set; }

    #endregion

    #region Status Information

    /// <summary>
    ///     Gets or sets the new status of the order after this change.
    /// </summary>
    /// <value>
    ///     An <see cref="OrderStatus"/> enum value representing the order's state after the transition.
    /// </value>
    public OrderStatus Status { get; set; }

    /// <summary>
    ///     Gets or sets an optional explanatory note or reason for the status change.
    /// </summary>
    /// <value>
    ///     A string containing additional context about why the status changed. Defaults to empty string.
    /// </value>
    [MaxLength(300)]
    public string Note { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the timestamp when this status change occurred.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime"/> value representing the exact moment of the status transition.
    /// </value>
    public DateTime ChangedAt { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the order associated with this history entry.
    /// </summary>
    /// <value>
    ///     An <see cref="Order"/> entity representing the order whose status changed.
    /// </value>
    public Order Order { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the user who performed the status change.
    /// </summary>
    /// <value>
    ///     A <see cref="User"/> entity, or <c>null</c> if the change was system-generated.
    /// </value>
    public User? ChangedByUser { get; set; }

    #endregion
}