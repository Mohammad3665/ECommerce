namespace ECommerce.Application.Common.Interfaces.Services;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    int GetMaxRoleLevel();
}