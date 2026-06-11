using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Domain.Entities.Application.Role;

/// <summary>
///     Represents a security role or permission group for user access control.
/// </summary>
public class Role : BaseEntity<long>
{
    #region Basic Information

    /// <summary>
    ///     Gets or sets the unique technical name identifier of the role.
    /// </summary>
    /// <value>
    ///     A string containing the system-level role name (e.g., "SuperAdmin", "ProductManager"). Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the human-readable display name for the role.
    /// </summary>
    /// <value>
    ///     A string containing the localized, user-friendly role title. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(100)]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the detailed description of the role's responsibilities and permissions.
    /// </summary>
    /// <value>
    ///     A string explaining what users with this role can do. Defaults to empty string.
    /// </value>
    [Required]
    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;

    #endregion

    #region Role Configuration

    /// <summary>
    ///     Gets or sets a value indicating whether this role is automatically assigned to new registered users.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this is the default role for new users; otherwise, <c>false</c>.
    /// </value>
    public bool IsDefault { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether this role is protected from deletion or modification.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the role cannot be deleted or have critical properties modified; otherwise, <c>false</c>.
    /// </value>
    public bool IsSystemProtected { get; set; }

    /// <summary>
    ///     Gets or sets the hierarchical level of the role for permission inheritance and priority.
    /// </summary>
    /// <value>
    ///     An integer representing the role's position in the hierarchy (higher number = more privileges).
    /// </value>
    public int Level { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the collection of user-role assignments for this role.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="UserRole"/> junction entities linking users to this role. Defaults to an empty list.
    /// </value>
    public ICollection<UserRole> UserRoles { get; set; } = [];

    /// <summary>
    ///     Gets or sets the collection of permissions assigned to this role.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="RolePermission"/> junction entities linking permissions to this role. Defaults to an empty list.
    /// </value>
    public ICollection<RolePermission> RolePermissions { get; set; } = [];

    #endregion
}