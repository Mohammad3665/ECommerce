using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities.Application.Invoice;

/// <summary>
///     Represents a financial invoice document for a customer order.
/// </summary>
public class Invoice : BaseEntity<long>
{
    #region Foreign Keys

    /// <summary>
    ///     Gets or sets the unique identifier of the order this invoice belongs to.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Order.Id"/>.
    /// </value>
    public long OrderId { get; set; }

    #endregion

    #region Document Information

    /// <summary>
    ///     Gets or sets the unique sequential identifier for the invoice.
    /// </summary>
    /// <value>
    ///     A string containing the invoice number in a standardized format. Must be unique.
    /// </value>
    public string InvoiceNumber { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the date when the invoice was issued.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime"/> value representing the invoice creation date in UTC.
    /// </value>
    public DateTime InvoiceDate { get; set; }

    /// <summary>
    ///     Gets or sets the URL or file path to the generated PDF version of the invoice.
    /// </summary>
    /// <value>
    ///     A string containing the path to the PDF document. Defaults to empty string.
    /// </value>
    public string InvoicePdfUrl { get; set; } = string.Empty;

    #endregion

    #region Financial Breakdown

    /// <summary>
    ///     Gets or sets the total price of all items before any discounts, taxes, or shipping.
    /// </summary>
    /// <value>
    ///     A decimal value representing the sum of (item price × quantity) for all order items.
    /// </value>
    public decimal SubTotal { get; set; }

    /// <summary>
    ///     Gets or sets the total discount applied to the order.
    /// </summary>
    /// <value>
    ///     A decimal value representing the reduction from subtotal before tax.
    /// </value>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    ///     Gets or sets the tax amount applied to the order.
    /// </summary>
    /// <value>
    ///     A decimal value representing calculated tax (VAT, GST, Sales Tax).
    /// </value>
    public decimal TaxAmount { get; set; }

    /// <summary>
    ///     Gets or sets the shipping cost for the order.
    /// </summary>
    /// <value>
    ///     A decimal value representing delivery fees.
    /// </value>
    public decimal ShippingCost { get; set; }

    /// <summary>
    ///     Gets or sets the final amount the customer needs to pay (or has paid).
    /// </summary>
    /// <value>
    ///     A decimal value representing the grand total after all calculations.
    /// </value>
    public decimal TotalAmount { get; set; }

    #endregion

    #region Status

    /// <summary>
    ///     Gets or sets the current state of the invoice in its lifecycle.
    /// </summary>
    /// <value>
    ///     An <see cref="InvoiceStatus"/> enum value (Issued, Paid, or Cancelled).
    /// </value>
    public InvoiceStatus Status { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the order associated with this invoice.
    /// </summary>
    /// <value>
    ///     An <see cref="Order"/> entity representing the underlying transaction.
    /// </value>
    public Order.Order Order { get; set; } = null!;

    #endregion
}