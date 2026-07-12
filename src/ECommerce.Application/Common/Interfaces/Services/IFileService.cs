using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Common.Interfaces.Services;

public record MultipleUploadResult(List<string> UploadedImages, List<string> Errors);

/// <summary>
///     Provides file operations including saving, deleting, and cleaning file names.
/// </summary>
public interface IFileService
{
    /// <summary>
    ///     Saves an uploaded file using a specified property of an entity as the file name.
    /// </summary>
    /// <typeparam name="TEntity">The entity type containing the property.</typeparam>
    /// <param name="file">The uploaded file to save.</param>
    /// <param name="entity">The entity instance to extract the file name from.</param>
    /// <param name="filePath">The relative directory path (e.g., "uploads/products").</param>
    /// <param name="propertyName">The property name to use as file name.</param>
    /// <returns>The saved file name with extension, or empty string if no file provided.</returns>
    Task<string> SaveFileAsync<TEntity>(IFormFile file, TEntity entity, string filePath, string propertyName);

    /// <summary>
    ///     Saves an uploaded file with the specified base name.
    /// </summary>
    /// <param name="file">The uploaded file to save.</param>
    /// <param name="fileName">The base file name without extension.</param>
    /// <param name="folerPath">The relative directory path (e.g., "uploads/products").</param>
    /// <returns>The saved file name with extension, or empty string if no file provided.</returns>
    Task<string> SaveFileAsync(IFormFile file, string fileName, string folderPath);

    /// <summary>
    ///     Deletes a file from the specified directory path.
    /// </summary>
    /// <param name="relativeFilePath">The relative directory path where the file is located.</param>
    void DeleteFile(string relativeFilePath);

    Task DeleteFilesAsync(IEnumerable<string> paths, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Sanitizes a file name by removing invalid characters and limiting length.
    /// </summary>
    /// <param name="fileName">The file name to clean.</param>
    /// <returns>A sanitized file name safe for file system operations.</returns>
    string CleanFileName(string fileName);

    /// <summary>
    ///     Uploads a single image with validation for size, format, and dimensions.
    /// </summary>
    /// <param name="file">The image file to upload.</param>
    /// <param name="folderName">The target folder name under the uploads directory.</param>
    /// <returns>
    ///     The generated file name if upload succeeds; otherwise, <c>null</c> if validation fails.
    /// </returns>
    Task<string?> UploadImageValidatedAsync(IFormFile file, string folderName);

    /// <summary>
    ///     Uploads multiple images with validation applied to each file.
    /// </summary>
    /// <param name="files">The list of image files to upload.</param>
    /// <param name="folderName">The target folder name under the uploads directory.</param>
    /// <returns>
    ///     A <see cref="MultipleUploadResult"/> containing the results of the upload operation,
    ///     including successfully uploaded file names and any validation errors.
    /// </returns>
    Task<MultipleUploadResult> UploadMultipleImagesValidatedAsync(List<IFormFile> files, string folderName);
}