using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

[ApiController]
[Area("Admin")]
[Authorize(Policy = "AdminAndAbove")]
[Route(template: "Api/[area]/[controller]/[action]")]
public abstract class AdminBaseController : ControllerBase {}