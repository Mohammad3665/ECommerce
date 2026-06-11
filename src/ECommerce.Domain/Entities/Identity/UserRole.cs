using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ECommerce.Domain.Entities.Application.Role;
using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Domain.Entities.Identity;

/// <summary>
///     Represents the junction (many-to-many) relationship between users and roles with audit metadata.
/// </summary>
public class UserRole
{
    #region Foreign Keys

    /// <summary>
    ///     Gets or sets the unique identifier of the user being assigned a role.
    /// </summary>
    /// <value>
    ///     A <see cref="Guid"/> value referencing <see cref="User.Id"/>.
    /// </value>
    [ForeignKey(nameof(User))]
    public Guid UserId { get; set; }

    /// <summary>
    ///     Gets or sets the unique identifier of the role being assigned to the user.
    /// </summary>
    /// <value>
    ///     A <see cref="long"/> value referencing <see cref="Role.Id"/>.
    /// </value>
    [ForeignKey(nameof(Role))]
    public long RoleId { get; set; }

    #endregion

    #region Audit Information

    /// <summary>
    ///     Gets or sets the timestamp when this role was assigned to the user.
    /// </summary>
    /// <value>
    ///     A <see cref="DateTime"/> value representing the assignment time in UTC.
    /// </value>
    [Required]
    public DateTime AssignedAt { get; set; }

    /// <summary>
    ///     Gets or sets the identifier of the administrator who assigned this role to the user.
    /// </summary>
    /// <value>
    ///     A nullable <see cref="Guid"/> referencing <see cref="User.Id"/>.
    ///     <c>null</c> indicates the assignment was done automatically by the system (e.g., default role on registration).
    /// </value>
    [ForeignKey(nameof(User))]
    public Guid? AssignedByUserId { get; set; }

    #endregion

    #region Relations

    /// <summary>
    ///     Gets or sets the user who receives this role assignment.
    /// </summary>
    /// <value>
    ///     A <see cref="User"/> entity representing the assigned user.
    /// </value>
    public User User { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the role that is assigned to the user.
    /// </summary>
    /// <value>
    ///     A <see cref="Role"/> entity representing the assigned role.
    /// </value>
    public Role Role { get; set; } = null!;

    /// <summary>
    ///     Gets or sets the administrator who performed this role assignment.
    /// </summary>
    /// <value>
    ///     A <see cref="User"/> entity representing the assigning administrator, or <c>null</c> if system-assigned.
    /// </value>
    public User? AssignedBy { get; set; }

    #endregion
}