namespace ECommerce.Application.Common.Interfaces.Services;

/// <summary>
/// Provides file system path resolution services for the application.
/// </summary>
/// <remarks>
/// This interface is designed to abstract physical path resolution across different environments.
/// Implementations handle the underlying file system operations and environment-specific path differences.
/// Common use cases include file uploads, template rendering, and static asset management.
/// </remarks>
public interface IPathService
{
    /// <summary>
    /// Gets the absolute physical path to the web root directory.
    /// </summary>
    /// <returns>
    /// A string containing the full physical path to the wwwroot or web root folder.
    /// </returns>
    string GetWebRootPath();
}