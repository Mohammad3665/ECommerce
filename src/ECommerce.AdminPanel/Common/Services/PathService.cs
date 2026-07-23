using ECommerce.Application.Common.Interfaces.Services;

namespace ECommerce.AdminPanel.Common.Services;

public class PathService(IWebHostEnvironment environment) : IPathService
{
    public string GetWebRootPath()
    {
        return environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
    }
}