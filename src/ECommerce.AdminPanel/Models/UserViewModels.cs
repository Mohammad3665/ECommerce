namespace ECommerce.AdminPanel.Models;

public class UserListViewModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsEmailConfirmed { get; set; }
}

public class UserFormViewModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Password { get; set; }
    public string? RoleName { get; set; }
    public bool IsActive { get; set; } = true;
}
