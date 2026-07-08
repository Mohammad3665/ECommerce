namespace ECommerce.Application.Features.Slides.Commands.ToggleSlideStatus;

public class ToggleSlideStatusCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleSlideStatusCommand, Result>
{
    public async Task<Result> Handle(ToggleSlideStatusCommand request, CancellationToken cancellationToken)
    {
        var slide = await unitOfWork.SlideRepository.GetAsync(
            expression: s => s.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (slide is null)
            return new Error("Slide.NotFound", "اسلاید یافت نشد.", ErrorType.NotFound);

        slide.IsActive = request.IsActive;
        unitOfWork.SlideRepository.Update(slide);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);

        return saveResult.IsFailure ?
            new Error("Slide.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected) :
            Result.Success();
    }
}
