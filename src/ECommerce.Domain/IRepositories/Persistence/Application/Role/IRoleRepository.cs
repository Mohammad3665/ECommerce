namespace ECommerce.Domain.IRepositories.Persistence.Application.Role;

public interface IRoleRepository : IBaseRepository<long, Entities.Application.Role.Role>
{
    /// <summary>
    ///     Retrieves all user-role assignments for a specific role asynchronously.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="cancellationToken">
    ///     A token to cancel the asynchronous operation. Default is <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a list of <see cref="IdentityUserRole{long}"/> entities associated with the specified role.
    /// </returns>
    /// <exception cref="OperationCanceledException">
    ///     Thrown when the operation is canceled via <paramref name="cancellationToken"/>.
    /// </exception>
    Task<List<UserRole>> GetUserRolesByRoleIdAsync(long roleId, CancellationToken cancellationToken);

    /// <summary>
    ///     Updates the user-role assignments with the provided collection.
    /// </summary>
    /// <param name="userRoles">
    ///     A list of <see cref="IdentityUserRole{long}"/> entities representing the new user-role assignments.
    /// </param>
    /// <remarks>
    ///     This method performs a bulk update operation and does not return any value.
    ///     It will replace all existing assignments with the provided list.
    /// </remarks>
    void UpdateUserRoles(List<UserRole> userRoles);
}