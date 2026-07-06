using ECommerce.Application.Dtos.Users;
using ECommerce.Application.Features.Users.Commands.EditUserProfile;

namespace ECommerce.Api.Controllers.v1;

public class UsersController(ISender sender, ILogger<UsersController> logger) : BaseController
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> EditProfile([FromBody] EditUserProfileRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<EditUserProfileCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}