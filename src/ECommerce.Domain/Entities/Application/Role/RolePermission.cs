using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Base;

namespace ECommerce.Domain.Entities.Application.Role;

/// <summary>
///     Represents the junction (many-to-many) relationship between roles and permissions in the RBAC system.
/// </summary>
public class RolePermission : BaseEntity<long>
{
    #region Foreign Keys

    /// <summary>
    ///     Gets or sets the unique identifier of the role receiving the permission.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Role.Id"/>.
    /// </value>
    [ForeignKey(nameof(Role))]
    public long RoleId { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier of the permission being granted to the role.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Permission.Id"/>.
    /// </value>
    [ForeignKey(nameof(Permission))]
    public long PermissionId { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the role that receives this permission.
    /// </summary>
    /// <value>
    ///     A <see cref="Role"/> entity representing the role being granted access.
    /// </value>
    public Role Role { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the permission that is granted to the role.
    /// </summary>
    /// <value>
    ///     A <see cref="Permission"/> entity representing the granted access right.
    /// </value>
    public Permission Permission { get; set; } = null!;

    #endregion
}