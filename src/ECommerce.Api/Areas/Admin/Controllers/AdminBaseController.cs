namespace ECommerce.Api.Areas.Admin.Controllers;

[ApiController]
[Area("Admin")]
[Authorize(Policy = "AdminPanelAccess")]
[Route(template: "Api/[area]/[controller]/[action]")]
public abstract class AdminBaseController : ControllerBase { }