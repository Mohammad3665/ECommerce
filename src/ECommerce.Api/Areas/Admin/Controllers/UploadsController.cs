namespace ECommerce.Api.Areas.Admin.Controllers;

public class UploadsController(IFileService fileService) : AdminBaseController
{
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    private const long MaxFileSizeInBytes = 2 * 1024 * 1024;

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file, string folderName = "products")
    {
        var relativeUrl = await fileService.UploadImageValidatedAsync(file, folderName);
        if (relativeUrl is null)
            return BadRequest("فایل ارسال شده معتبر نیست (حجم یا پسوند غیرمجاز).");
        return Ok(new { ImageUrl = relativeUrl });
    }

    [HttpPost]
    public async Task<IActionResult> UploadMultipleImages(List<IFormFile> files, string folderName = "products")
    {
        var result = await fileService.UploadMultipleImagesValidatedAsync(files, folderName);
        if (result.UploadedImages.Count == 0 && result.Errors.Count > 0)
        {
            return BadRequest(new { Message = "هیچ‌کدام از فایل‌ها واجد شرایط آپلود نبودند.", Errors = result.Errors });
        }
        return Ok(result);
    }
}