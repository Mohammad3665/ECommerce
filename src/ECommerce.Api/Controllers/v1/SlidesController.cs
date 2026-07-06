using ECommerce.Application.Features.Slides.Queries.GetActiveSlides;

namespace ECommerce.Api.Controllers.v1;

public class SlidesController(ISender sender, ILogger<SlidesController> logger) : BaseController
{
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetActiveSlidesQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }
}
