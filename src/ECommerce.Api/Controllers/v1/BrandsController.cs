using ECommerce.Application.Features.Brands.Queries.GetAllBrands;
using ECommerce.Application.Features.Brands.Queries.GetBrandBySlug;

namespace ECommerce.Api.Controllers.v1;

public class BrandsController(ISender sender, ILogger<BrandsController> logger) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetAllBrandsQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{slug}")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
    {
        var query = new GetBrandBySlugQuery(slug);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }
}
