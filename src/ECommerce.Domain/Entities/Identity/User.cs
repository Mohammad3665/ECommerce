using ECommerce.Domain.Entities.Application.Article;
using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Domain.Entities.Identity;

/// <summary>
///     Represents a user in the system.
/// </summary>
public class User : BaseEntity<Guid>
{
    #region Properties

    /// <summary>
        ///     Gets or sets the user full name.
        /// </summary>
        /// <value>
        ///     A string containing the user's first and last name. Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(150)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the email address of the user.
        /// </summary>
        /// <value>
        ///     A string representing the user's unique email. Defaults to empty string.
        /// </value>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the phone number of the user.
        /// </summary>
        /// <value>
        ///     A string containing the user's contact number. Defaults to empty string.
        /// </value>
    [Required]
    [MaxLength(11)]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the hashed password of the user.
        /// </summary>
        /// <value>
        ///     A string representing the securely hashed password. Never stores plain text.
        /// </value>
    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
        ///     Gets or sets the timestamp of the user's last successful login.
        /// </summary>
        /// <value>
        ///     A nullable <see cref="DateTime" /> object. Null if the user has never logged in.
        /// </value>
    public DateTime? LastLoginAt { get; set; }

    /// <summary>
        ///     Gets or sets a value indicating whether the user account is active.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the user can log in and use the system; otherwise, <c>false</c>.
        /// </value>
    public bool IsActive { get; set; } = true;

    #endregion 

    #region Security Properties

    /// <summary>
        ///     Gets or sets the security code for two-factor authentication or account verification.
        /// </summary>
        /// <value>
        ///     A nullable <see cref="Guid" /> representing the one-time security token.
        /// </value>
    public Guid? SecurityCode { get; set; }

    /// <summary>
        ///     Gets or sets the expiration time for the <see cref="SecurityCode" />.
        /// </summary>
        /// <value>
        ///     A nullable <see cref="DateTime" /> indicating when the security code becomes invalid.
        /// </value>
    public DateTime? SecurityCodeExpiry { get; set; }

    /// <summary>
        ///     Gets or sets a value indicating whether the user's email address has been confirmed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if email is verified; otherwise, <c>false</c>. Default is <c>false</c>.
        /// </value>
    public bool IsEmailConfirmed { get; set; } = false;

    /// <summary>
    /// Gets or sets  a value indicating whether the refresh token.
    /// </summary>
    /// <value>
    /// A nullable <see cref="string" /> indicating when token refreshed.
    /// </value>
    [MaxLength(256)]
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// Gets or sets  a value indicating whether the refresh token expiry time.
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime" /> indicating when token refreshed.
    /// </value>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the collection of roles assigned to this user.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="Role" /> entities. Defaults to an empty list.
    /// </value>
    public ICollection<UserRole> UserRoles { get; set; } = [];

    /// <summary>
        ///     Gets or sets the collection of orders placed by this user.
        /// </summary>
        /// <value>
        ///     A collection of <see cref="Order" /> entities. Defaults to an empty list.
        /// </value>
    public ICollection<Order.Order> Orders { get; set; } = [];

    /// <summary>
        ///     Gets or sets the collection of comments written by this user.
        /// </summary>
        /// <value>
        ///     A collection of <see cref="Comment" /> entities. Defaults to an empty list.
        /// </value>
    public ICollection<Comment> Comments { get; set; } = [];

    /// <summary>
        ///     Gets or sets the collection of articles authored by this user.
        /// </summary>
        /// <value>
        ///     A collection of <see cref="Article" /> entities. Defaults to an empty list.
        /// </value>
    public ICollection<Article> Articles { get; set; } = [];

    #endregion
}