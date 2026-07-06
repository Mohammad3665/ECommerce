using ECommerce.Application.Dtos.Comments;
using ECommerce.Application.Features.Comments.Commands.CreateComment;
using ECommerce.Application.Features.Comments.Commands.DeleteComment;
using ECommerce.Application.Features.Comments.Queries.GetCommentsCount;
using ECommerce.Application.Features.Comments.Queries.GetTargetComments;

namespace ECommerce.Api.Controllers.v1;

public class CommentsController(ISender sender, ILogger<CommentsController> logger) : BaseController
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateCommentCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet]
    public async Task<IActionResult> GetTargetComments([FromQuery] GetTargetCommentsQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    // Get count
    [HttpGet]
    public async Task<IActionResult> Count([FromQuery] GetCommentsCountQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCommentCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}
