using System.Security.Claims;

namespace ECommerce.Domain.Common.Interfaces;

/// <summary>
/// Provides JWT (JSON Web Token) operations for authentication and authorization
/// </summary>
/// <remarks>
/// This interface is responsible for generating access tokens, refresh tokens,
/// and extracting user information from expired tokens.
/// </remarks>
public interface IJwtProvider
{
    /// <summary>
    /// Generates a JWT access token for the authenticated user
    /// </summary>
    /// <param name="userId">The unique identifier of the user</param>
    /// <param name="email">The email address of the user</param>
    /// <param name="roles">The collection of roles assigned to the user</param>
    /// <param name="permissions">The collection of permissions assigned to the user</param>
    /// <returns>
    /// A JWT string that can be used for API authentication
    /// </returns>
    string GenerateToken(Guid userId, string email, IEnumerable<string> roles, IEnumerable<string> permissions);

    /// <summary>
    /// Generates a cryptographically secure refresh token for session renewal
    /// </summary>
    /// <returns>
    /// A random string that serves as a refresh token
    /// </returns>
    /// <remarks>
    /// Refresh tokens are typically stored client-side (in cookies or local storage)
    /// and are used to obtain new access tokens without re-authentication.
    /// </remarks>
    string GenerateRefreshToken();

    /// <summary>
    /// Retrieves the user claims principal from an expired JWT token
    /// </summary>
    /// <param name="token">The expired JWT token</param>
    /// <returns>
    /// A <see cref="ClaimsPrincipal"/> containing the user's claims if successful;
    /// otherwise, <c>null</c>
    /// </returns>
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}