namespace ECommerce.Application.Features.Comments.Commands.DeleteCommentByAdmin;

public class DeleteCommentByAdminCommandValidator : AbstractValidator<DeleteCommentByAdminCommand>
{
    public DeleteCommentByAdminCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithName("آیدی");
    }
}
