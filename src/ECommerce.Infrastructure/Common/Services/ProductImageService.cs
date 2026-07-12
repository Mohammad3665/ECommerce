using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Infrastructure.Common.Services;

public sealed class ProductImageService() : IProductImageService
{
    public async Task<IReadOnlyCollection<string>> UpdateImagesAsync(Product product, ICollection<ProductImageDto> requestImages, CancellationToken cancellationToken = default)
    {
        var dbImages = product.Images.ToList();

        var requestLookup = requestImages.ToDictionary(x => Path.GetFileName(x.ImageUrl).Trim().ToLower());
        var dbLookup = dbImages.ToDictionary(x => Path.GetFileName(x.ImageUrl).Trim().ToLower());
        var deletedPhysicalFiles = new List<string>();

        #region Delete Removed Images

        foreach (var dbImage in dbImages)
        {
            var fileName = Path.GetFileName(dbImage.ImageUrl).Trim().ToLower();

            if (!requestLookup.ContainsKey(fileName))
            {
                deletedPhysicalFiles.Add(
                    dbImage.ImageUrl
                        .Replace("\\", "/")
                        .TrimStart('/'));

                product.Images.Remove(dbImage);
            }
        }

        #endregion

        #region Add New Images

        foreach (var requestImage in requestImages)
        {
            var fileName = Path.GetFileName(requestImage.ImageUrl).Trim().ToLower();

            if (!dbLookup.ContainsKey(fileName))
            {
                product.Images.Add(new ProductImage
                {
                    ImageUrl = requestImage.ImageUrl,
                    IsMain = requestImage.IsMain,
                    DisplayOrder = requestImage.DisplayOrder
                });
            }
        }

        #endregion

        #region Update Existing Images

        foreach (var dbImage in product.Images)
        {
            var fileName = Path.GetFileName(dbImage.ImageUrl).Trim().ToLower();

            if (!requestLookup.TryGetValue(fileName, out var requestImage))
                continue;

            dbImage.IsMain = requestImage.IsMain;
            dbImage.DisplayOrder = requestImage.DisplayOrder;
        }

        #endregion

        return await Task.FromResult<IReadOnlyCollection<string>>(deletedPhysicalFiles);
    }
}