namespace ECommerce.Domain.Entities.Application.Role;

/// <summary>
///     Represents a specific action or operation that can be performed in the system.
/// </summary>
public class Permission : BaseEntity<long>
{
    #region Basic Information

    /// <summary>
    ///     Gets or sets the unique technical identifier of the permission.
    /// </summary>
    /// <value>
    ///     A string containing the permission key in dot notation format. Defaults to empty string.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the category or resource group that this permission belongs to.
    /// </summary>
    /// <value>
    ///     A string containing the module name for logical grouping. Defaults to empty string.
    /// </value>
    public string Module { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the detailed explanation of what this permission allows.
    /// </summary>
    /// <value>
    ///     A string containing the human-readable description of the permission. Defaults to empty string.
    /// </value>
    public string Description { get; set; } = string.Empty;

    #endregion

    #region Rlation

    /// <summary>
    ///     Gets or sets the collection of role-permission assignments for this permission.
    /// </summary>
    /// <value>
    ///     A collection of <see cref="RolePermission"/> junction entities linking roles to this permission. Defaults to an empty list.
    /// </value>
    public ICollection<RolePermission> RolePermissions { get; set; } = [];

    #endregion
}