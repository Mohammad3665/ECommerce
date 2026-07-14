namespace ECommerce.Infrastructure.Options;

/// <summary>
///     JWT authentication configuration settings.
///     Bound from appsettings.json under "JwtSettings" section.
/// </summary>
public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    /// <summary>Whether to validate the token issuer.</summary>
    public bool ValidateIssuer { get; set; }

    /// <summary>Whether to validate the token audience.</summary>
    public bool ValidateAudience { get; set; }

    /// <summary>The token issuer (e.g., API URL).</summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>The token audience (e.g., application name).</summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>The secret key used for signing tokens (minimum 256-bit).</summary>
    public string Secret { get; set; } = string.Empty;

    /// <summary>Token expiration time in minutes.</summary>
    public int ExpirationInMinutes { get; set; }
}