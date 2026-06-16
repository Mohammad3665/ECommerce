using ECommerce.Application.Common.Interfaces.Services;

namespace ECommerce.Infrastructure.Common.Services;

/// <summary>
/// Implementation of <see cref="IPasswordService"/> using BCrypt for secure password hashing.
/// </summary>
public class BCryptPasswordService : IPasswordService
{
    private const int WorkFactor = 12;

    /// <summary>
    /// Hashes the provided password using BCrypt with the specified work factor.
    /// </summary>
    /// <param name="password">The plain-text password to hash.</param>
    /// <returns>The BCrypt-hashed password as a string.</returns>
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: WorkFactor);
    }

    /// <summary>
    /// Verifies if the provided plain-text password matches the BCrypt-hashed password.
    /// </summary>
    /// <param name="password">The plain-text password to verify.</param>
    /// <param name="hashedPassword">The BCrypt-hashed password to compare against.</param>
    /// <returns>True if the password matches the hash; otherwise, false.</returns>
    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }

}
