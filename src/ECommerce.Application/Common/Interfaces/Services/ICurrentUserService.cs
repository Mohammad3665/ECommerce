namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
/// Provides access to the currently authenticated user's context and permissions.
/// </summary>
/// <remarks>
/// This interface is designed to retrieve information about the current user making the request.
/// Implementations typically extract user data from the HTTP context, JWT tokens, or session.
/// All methods and properties are thread-safe and can be used anywhere in the application layer.
/// </remarks>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the unique identifier of the currently authenticated user.
    /// </summary>
    /// <value>
    /// A nullable Guid representing the user's unique identifier.
    /// </value>
    Guid? UserId { get; }

    /// <returns>
    /// An integer representing the highest role level assigned to the user.
    /// <para>Higher numbers indicate higher privileges.</para>
    /// <para>Example: 1 = User, 5 = Moderator, 10 = Administrator</para>
    /// </returns>
    int GetMaxRoleLevel();

    /// <summary>
    /// Determines whether the current user has a specific permission.
    /// </summary>
    /// <param name="permission">
    /// The permission name to check.
    /// <para>Example: "products.create", "comments.reject", "articles.update"</para>
    /// </param>
    /// <returns>
    /// <c>true</c> if the current user has the specified permission; otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    bool HasPermission(string permission);
}