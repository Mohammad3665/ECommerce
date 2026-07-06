using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace ECommerce.Infrastructure.Identity.Providers;

/// <summary>
/// Provides dynamic authorization policies for permissions.
/// </summary>
/// <remarks>
/// Creates policies on-demand for permission-based authorization.
/// </remarks>
public class PermissionPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if (policy is not null) return policy;

        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}

public record PermissionRequirement(string Permission) : IAuthorizationRequirement;