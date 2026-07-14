namespace ECommerce.Domain.Entities.Order;

/// <summary>
///     Represents payment information and transaction details for an order.
/// </summary>
public class OrderPayment : BaseEntity<long>
{
    #region Payment Information

    /// <summary>
    ///     Gets or sets the payment method used by the customer.
    /// </summary>
    /// <value>
    ///     A string representing the payment gateway or method. Defaults to empty string.
    /// </value>
    public string PaymentMethod { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the unique transaction identifier from the payment gateway.
    /// </summary>
    /// <value>
    ///     A string containing the gateway's transaction reference number. Defaults to empty string.
    /// </value>
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets a value indicating whether the payment has been successfully completed.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the payment is confirmed and funds are received; otherwise, <c>false</c>.
    /// </value>
    public bool IsPaid { get; set; }

    /// <summary>
    ///     Gets or sets the timestamp when the payment was successfully confirmed.
    /// </summary>
    /// <value>
    ///     A nullable <see cref="DateTime"/> representing the payment confirmation time.
    ///     <c>null</c> indicates payment has not been completed yet.
    /// </value>
    public DateTime? PaidAt { get; set; }

    #endregion

    #region ForigenKeys

    /// <summary>
    ///     Gets or sets the foreign key referencing the associated order.
    /// </summary>
    /// <value>
    ///     The unique identifier of the order that this shipping information belongs to.
    /// </value>
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
