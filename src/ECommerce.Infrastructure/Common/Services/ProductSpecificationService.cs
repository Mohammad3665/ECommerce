using ECommerce.Application.Common.Extensions;
using ECommerce.Application.Common.Interfaces.Services;
using ECommerce.Application.Dtos.Products;
using ECommerce.Domain.Entities.Product;

namespace ECommerce.Infrastructure.Common.Services;

public class ProductSpecificationService : IProductSpecificationService
{
    public Task SyncAsync(Product product, ICollection<SpecificationDto> specifications, CancellationToken cancellationToken = default)
    {
        var dbSpecifications = product.Specifications.ToList();

        var requestLookup = specifications.ToDictionary(
            x => x.Key.NormalizePersian(),
            x => x);

        var dbLookup = dbSpecifications.ToDictionary(
            x => x.Key.NormalizePersian(),
            x => x);

        #region Delete

        foreach (var dbSpec in dbSpecifications)
        {
            if (!requestLookup.ContainsKey(dbSpec.Key.NormalizePersian()))
            {
                product.Specifications.Remove(dbSpec);
            }
        }

        #endregion

        #region Add or Update
        
        foreach (var requestSpec in specifications)
        {
            var key = requestSpec.Key.NormalizePersian();

            if (dbLookup.TryGetValue(key, out var dbSpec))
            {
                dbSpec.Value = requestSpec.Value.NormalizePersian();
            }
            else
            {
                product.Specifications.Add(new ProductSpecification
                {
                    Key = key,
                    Value = requestSpec.Value.NormalizePersian()
                });
            }
        }

        #endregion

        return Task.CompletedTask;
    }
}