namespace ECommerce.Application.Features.Comments.Commands.ChangeCommentStatus;

public class ChangeCommentStatusCommandValidator : AbstractValidator<ChangeCommentStatusCommand>
{
    public ChangeCommentStatusCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithName("آیدی");
    }
}
