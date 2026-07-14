using ECommerce.Application.Authorization;
using ECommerce.Application.Features.Comments.Commands.ChangeCommentStatus;
using ECommerce.Application.Features.Comments.Commands.DeleteCommentByAdmin;
using ECommerce.Application.Features.Comments.Commands.RejectComment;
using ECommerce.Application.Features.Comments.Queries.AdminGetCommentsCount;
using ECommerce.Application.Features.Comments.Queries.GetAllComments;
using ECommerce.Application.Features.Comments.Queries.GetPendingComments;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class CommentsController(ISender sender, ILogger<CommentsController> logger) : AdminBaseController
{
    // Get Pending
    [HttpGet]
    [HasPermission(Permissions.Comments.Read)]
    public async Task<IActionResult> Pending([FromQuery] GetPendingCommentsQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet]
    [HasPermission(Permissions.Comments.Read)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCommentsQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{id:guid}")]
    [HasPermission(Permissions.Comments.Approve)]
    public async Task<IActionResult> Approve(Guid id, [FromBody] bool isApproved, CancellationToken cancellationToken)
    {
        var command = new ChangeCommentStatusCommand(id, isApproved);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Comments.Delete)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCommentByAdminCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet]
    [HasPermission(Permissions.Comments.Read)]
    public async Task<IActionResult> Count([FromQuery] AdminGetCommentsCountQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(Permissions.Comments.Reject)]
    public async Task<IActionResult> Reject(Guid id, CancellationToken cancellationToken)
    {
        var command = new RejectCommentCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}