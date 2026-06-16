namespace ECommerce.Domain.Common.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, string email, IEnumerable<string> roles);
}