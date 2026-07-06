using System.Security.Claims;
using ECommerce.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Infrastructure.Common.Services;

public class CurrenUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId
    {
        get
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdClaim, out var parsedGuid) ? parsedGuid : null;
        }
    }

    public int GetMaxRoleLevel()
    {
        var roleClaim = httpContextAccessor.HttpContext?.User?.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

        if (string.IsNullOrEmpty(roleClaim))
        {
            roleClaim = httpContextAccessor.HttpContext?.User?.FindFirst("role")?.Value;
        }

        if (string.IsNullOrEmpty(roleClaim))
        {
            return 0;
        }

        return roleClaim switch
        {
            "SuperAdmin" => 100,
            "Admin" => 80,
            "ContentManager" => 50,
            "Customer" => 10,
            _ => 0
        };
    }

    public bool HasPermission(string permission)
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user is null) return false;

        bool haspermission = user.Claims.Any(c =>
            c.Type.Equals("permission", StringComparison.OrdinalIgnoreCase) &&
            c.Value == permission
        );
        return haspermission;
    }
}