namespace ECommerce.Domain.Enums;

/// <summary>
///     Represents the status lifecycle of a customer order.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    ///     Order has been created but payment has not been completed yet.
    /// </summary>
    /// <remarks>
    ///     In this state, the order can be modified or cancelled by the user.
    ///     Cart items are reserved but not deducted from inventory.
    ///     Orders in Pending state older than 24 hours are typically auto-cancelled.
    /// </remarks>
    Pending,

    /// <summary>
    ///     Payment has been successfully received and verified.
    /// </summary>
    Paid,

    /// <summary>
    ///     Order is being prepared for shipment (packing, quality check).
    /// </summary>
    Processing,

    /// <summary>
    ///     Order has been handed over to the shipping carrier and is in transit.
    /// </summary>
    Shipping,

    /// <summary>
    ///     Order has been successfully delivered to the customer.
    /// </summary>
    Delivered,

    /// <summary>
    ///     Order has been cancelled before delivery.
    /// </summary>
    Cancelled
}