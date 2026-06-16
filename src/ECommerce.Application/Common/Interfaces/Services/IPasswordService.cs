namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
/// Provides methods for password hashing and verification.
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Hashes the provided password using a secure hashing algorithm.
    /// </summary>
    /// <param name="password">The plain-text password to hash.</param>
    /// <returns>The hashed password as a string.</returns>
    string Hash(string password);

    /// <summary>
    /// Verifies if the provided plain-text password matches the hashed password.
    /// </summary>
    /// <param name="password">The plain-text password to verify.</param>
    /// <param name="hashedPassword">The hashed password to compare against.</param>
    /// <returns>True if the password matches the hash; otherwise, false.</returns>
    bool Verify(string password, string hashedPassword);
}