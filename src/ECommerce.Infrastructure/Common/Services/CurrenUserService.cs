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
        var levelClaim = httpContextAccessor.HttpContext?.User?.FindFirst("RoleLevel")?.Value;

        if (string.IsNullOrEmpty(levelClaim) || !int.TryParse(levelClaim, out int level))
        {
            return 0;
        }

        return level;
    }
}
