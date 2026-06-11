namespace ECommerce.Domain.Enums;

/// <summary>
///     Represents the type of discount applied by a coupon.
/// </summary>
public enum CouponType
{
    /// <summary>
    ///     Percentage-based discount (e.g., 10% off).
    /// </summary>
    Percentage,

    /// <summary>
    ///     Fixed amount discount (e.g., $20 off).
    /// </summary>
    FixedAmount
}