using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Admin.Controllers;

[ApiController]
[Area("Admin")]
[Authorize(Policy = "AdminPanelAccess")]
[Route(template: "Api/[area]/[controller]/[action]")]
public abstract class AdminBaseController : ControllerBase { }