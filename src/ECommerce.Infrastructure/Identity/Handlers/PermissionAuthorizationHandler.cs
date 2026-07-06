using ECommerce.Infrastructure.Identity.Providers;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Infrastructure.Identity.Handlers;

/// <summary>
/// Handles authorization based on user permissions.
/// </summary>
/// <remarks>
/// Checks if the current user has the required permission claim.
/// </remarks>
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context.User.Claims
            .Where(c => c.Type == "permissions")
            .Select(c => c.Value)
            .ToList();

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
