namespace ECommerce.Application.Features.Comments.Commands.RejectComment;

public class RejectCommentCommandValidator : AbstractValidator<RejectCommentCommand>
{
    public RejectCommentCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithName("آیدی");
    }
}