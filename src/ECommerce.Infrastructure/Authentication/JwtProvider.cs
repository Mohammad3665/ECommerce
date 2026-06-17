using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Domain.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure.Authentication;

public class JwtProvider(IConfiguration configuration) : IJwtProvider
{
    public string GenerateToken(Guid userId, string email, IEnumerable<string> roles)
    {
        var secretKey = configuration["JwtSettings:Secret"]
            ?? throw new InvalidOperationException("JWT Secret key is missing.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var expiryMinutes = double.Parse(configuration["JwtSettings:ExpiryInMinutes"] ?? "10");

        var currentTime = DateTime.Now;
        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            notBefore: currentTime,
            expires: currentTime.AddMinutes(expiryMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}