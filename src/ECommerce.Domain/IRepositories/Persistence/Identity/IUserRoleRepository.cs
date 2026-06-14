using ECommerce.Domain.Entities.Identity;

namespace ECommerce.Domain.IRepositories.Persistence.Identity;

/// <summary>
/// Represents the repository interface for managing user-role relationships.
/// Provides methods for adding, removing, querying, and checking user role assignments.
/// </summary>
public interface IUserRoleRepository
{
    /// <summary>
    /// Adds a new user-role relationship asynchronously.
    /// </summary>
    /// <param name="userRole">The user-role relationship entity to add.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation. Default is <see cref="CancellationToken.None"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userRole"/> is null.</exception>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via <paramref name="cancellationToken"/>.</exception>
    Task AddAsync(UserRole userRole, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes an existing user-role relationship.
    /// </summary>
    /// <param name="userRole">The user-role relationship entity to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="userRole"/> is null.</exception>
    /// <remarks>
    /// This method is synchronous and should be used in conjunction with <see cref="AddAsync"/> and <see cref="UnitOfWork.CommitAsync"/>.
    /// </remarks>
    void Remove(UserRole userRole);

    /// <summary>
    /// Retrieves a user-role relationship by user ID and role ID asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation. Default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains the <see cref="UserRole"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via <paramref name="cancellationToken"/>.</exception>
    Task<UserRole?> GetAsync(Guid userId, long roleId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all role assignments for a specific user asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation. Default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a list of <see cref="UserRole"/> entities for the specified user.
    /// Returns an empty list if the user has no role assignments.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via <paramref name="cancellationToken"/>.</exception>
    Task<List<UserRole>> GetByUserId(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether a specific user is assigned to a specific role asynchronously.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation. Default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result is <c>true</c> if the user is in the specified role; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="OperationCanceledException">Thrown when the operation is canceled via <paramref name="cancellationToken"/>.</exception>
    Task<bool> IsUserInRoleAsync(Guid userId, long roleId, CancellationToken cancellationToken = default);
}