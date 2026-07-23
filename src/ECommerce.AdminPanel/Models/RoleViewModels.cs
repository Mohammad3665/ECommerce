namespace ECommerce.AdminPanel.Models;

public class RoleListViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool IsDefault { get; set; }
}

public class RoleFormViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool IsDefault { get; set; }
    public bool GrantAllPermissions { get; set; }
    public List<long>? SelectedPermissionIds { get; set; } = [];
}

public class PermissionViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
