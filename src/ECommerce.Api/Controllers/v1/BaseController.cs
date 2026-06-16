using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.v1;

[ApiController]
[ApiVersion(1)]
public abstract class BaseController : ControllerBase
{
    
}