using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Areas.Controllers;

[ApiController]
[Authorize(Policy = "AdminAndAbove")]
public abstract class AdminBaseController : ControllerBase {}