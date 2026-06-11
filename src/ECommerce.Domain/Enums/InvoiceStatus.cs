namespace ECommerce.Domain.Enums;

/// <summary>
///     Represents the status lifecycle of a financial invoice in the order management system.
/// </summary>
public enum InvoiceStatus
{
    /// <summary>
    ///     Invoice has been created and issued to the customer.
    /// </summary>
    Issued,

    /// <summary>
    ///     Invoice has been fully paid by the customer.
    /// </summary>
    Paid,

    /// <summary>
    ///     Invoice has been cancelled and is no longer valid.
    /// </summary>
    Cancelled
}