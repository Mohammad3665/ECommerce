using Asp.Versioning;

namespace ECommerce.Api.Controllers.v1;

[ApiController]
[ApiVersion(1)]
[Route(template: "Api/V{version:apiVersion}/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    
}