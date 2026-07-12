namespace ECommerce.Api.Common.Services;

public class FileService(IPathService pathService) : IFileService
{
    public async Task<string> SaveFileAsync<TEntity>(IFormFile file, TEntity entity, string filePath, string propertyName = "Name")
    {
        if (file is null || file.Length == 0)
            return string.Empty;

        var property = typeof(TEntity).GetProperty(propertyName);
        if (property == null)
            throw new ArgumentException($"Property '{propertyName}' not found on type '{typeof(TEntity).Name}'");
        var fileName = property.GetValue(entity)?.ToString();
        if (string.IsNullOrEmpty(fileName))
            fileName = Guid.NewGuid().ToString();

        return await SaveFileAsync(file, fileName, filePath);
    }
    public async Task<string> SaveFileAsync(IFormFile file, string fileName, string folderPath)
    {
        if (file is null || file.Length == 0)
            return string.Empty;

        var webRootPath = pathService.GetWebRootPath();
        var absoluteUploadPath = Path.Combine(webRootPath, folderPath);

        if (!Directory.Exists(absoluteUploadPath))
            Directory.CreateDirectory(absoluteUploadPath);

        var extension = Path.GetExtension(file.FileName);
        var cleanFileName = CleanFileName(fileName);
        var finalFileName = $"{cleanFileName}{extension}";
        var fullPath = Path.Combine(absoluteUploadPath, finalFileName);

        var counter = 1;
        while (File.Exists(fullPath))
        {
            finalFileName = $"{cleanFileName}_{counter}{extension}";
            fullPath = Path.Combine(absoluteUploadPath, finalFileName);
            counter++;
        }

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return Path.Combine(folderPath, finalFileName).Replace("\\", "/");
    }

    public void DeleteFile(string relativeFilePath)
    {
        if (string.IsNullOrEmpty(relativeFilePath) || relativeFilePath.ToLower() == "none")
            return;

        var safeRelativePath = relativeFilePath.Replace("\\", "/").Trim();

        if (safeRelativePath.StartsWith("/"))
        {
            safeRelativePath = safeRelativePath.TrimStart('/');
        }

        var fullPath = Path.Combine(pathService.GetWebRootPath(), safeRelativePath);


        if (File.Exists(fullPath))
            File.Delete(fullPath);
    }

    public string CleanFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var cleanName = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));

        cleanName = cleanName.Replace(" ", "-");

        if (cleanName.Length > 100)
            cleanName = cleanName.Substring(0, 100);

        return cleanName;
    }

    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxLength = 2 * 1024 * 1024;

    public async Task<string?> UploadImageValidatedAsync(IFormFile file, string folderName)
    {
        if (file is null || file.Length == 0 || file.Length > MaxLength) return null;

        var ext = Path.GetExtension(file.FileName).ToLower();
        if (!AllowedExtensions.Contains(ext)) return null;

        string uniqueFileName = $"img_{Guid.NewGuid():N}";
        string targetFolder = $"uploads/{folderName.Trim().ToLower()}";

        return await SaveFileAsync(file, uniqueFileName, targetFolder); // همان متد ذخیره قبلی
    }

    public async Task<MultipleUploadResult> UploadMultipleImagesValidatedAsync(List<IFormFile> files, string folderName)
    {
        var uploadedUrls = new List<string>();
        var errors = new List<string>();

        if (files == null || files.Count == 0)
        {
            return new MultipleUploadResult(uploadedUrls, errors);
        }

        foreach (var file in files)
        {
            if (file is null || file.Length == 0) continue;

            if (file.Length > MaxLength)
            {
                errors.Add($"فایل {file.FileName} بیشتر از ۲ مگابایت است.");
                continue;
            }

            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!AllowedExtensions.Contains(ext))
            {
                errors.Add($"فرمت فایل {file.FileName} مجاز نیست. فقط (jpg, jpeg, png, webp)");
                continue;
            }

            string uniqueFileName = $"img_{Guid.NewGuid():N}";
            string targetFolder = $"uploads/{folderName.Trim().ToLower()}";

            string relativeUrl = await SaveFileAsync(file, uniqueFileName, targetFolder);

            if (!string.IsNullOrEmpty(relativeUrl))
            {
                uploadedUrls.Add(relativeUrl);
            }
            else
            {
                errors.Add($"خطایی در ذخیره فیزیکی فایل {file.FileName} رخ داد.");
            }
        }

        return new MultipleUploadResult(uploadedUrls, errors);
    }

    public Task DeleteFilesAsync(IEnumerable<string> paths, CancellationToken cancellationToken = default)
    {
        foreach (var path in paths)
            DeleteFile(path);

        return Task.CompletedTask;
    }
}