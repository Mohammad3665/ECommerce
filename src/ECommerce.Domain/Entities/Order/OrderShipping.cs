namespace ECommerce.Domain.Entities.Order;

/// <summary>
///     Represents shipping and delivery information for a customer order.
/// </summary>
/// <remarks>
///     This entity inherits from <see cref="BaseEntity{TKey}" /> where TKey is <see cref="long" />.
///     Stores the customer's shipping details at the time of order placement.
///     This is a snapshot to preserve address information even if the user updates their profile later.
///     Typically has a one-to-one relationship with <see cref="Order"/>.
/// </remarks>
public class OrderShipping : BaseEntity<long>
{
    #region Recipient Information

    /// <summary>
    ///     Gets or sets the full name of the recipient for shipping.
    /// </summary>
    /// <value>
    ///     A string containing the complete name of the person receiving the package. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the contact number of the shipping recipient.
    /// </summary>
    /// <value>
    ///     A string containing the phone number for delivery coordination. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(11)]
    public string PhoneNumber { get; set; } = string.Empty;

    #endregion

    #region Address Information

    /// <summary>
    ///     Gets or sets the full street address for delivery.
    /// </summary>
    /// <value>
    ///     A string containing the complete address including street name, building number, unit/apartment, and landmarks. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the postal/zip code for the shipping address.
    /// </summary>
    /// <value>
    ///     A string containing the postal code for accurate address identification. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; } = string.Empty;

    #endregion

    #region ForigenKeys

    /// <summary>
    ///     Gets or sets the foreign key referencing the associated order.
    /// </summary>
    /// <value>
    ///     The unique identifier of the order that this shipping information belongs to.
    /// </value>
    [ForeignKey(nameof(Order))]
    public long OrderId { get; set; }

    /// <summary>
    ///     Gets or sets the associated order entity.
    /// </summary>
    /// <value>
    ///     The <see cref="Order"/> entity that this shipping information is linked to.
    ///     This property is required and cannot be null.
    /// </value>
    public Order Order { get; set; } = null!;
    #endregion
}