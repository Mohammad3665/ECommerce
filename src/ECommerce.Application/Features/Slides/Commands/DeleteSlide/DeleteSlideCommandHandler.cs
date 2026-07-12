using ECommerce.Domain.Events.Slide;

namespace ECommerce.Application.Features.Slides.Commands.DeleteSlide;

public class DeleteSlideCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteSlideCommand, Result>
{
    public async Task<Result> Handle(DeleteSlideCommand request, CancellationToken cancellationToken)
    {
        var slide = await unitOfWork.SlideRepository.GetAsync(
            expression: s => s.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (slide is null)
            return new Error("Slide.NotFound", "اسلاید یافت نشد.", ErrorType.NotFound);

        string imagePathToDelete = slide.ImageUrl;
        var slidesToShift = await unitOfWork.SlideRepository.GetAllWithTrackingAsync(
            expression: s => s.DisplayOrder > slide.DisplayOrder,
            cancellationToken: cancellationToken
        );

        foreach (var slideToShift in slidesToShift)
            slideToShift.DisplayOrder--;

        unitOfWork.SlideRepository.DeletePermanently(slide);

        slide.AddDomainEvent(new SlideDeletedDomainEvent(imagePathToDelete));

        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
            return new Error("Slide.Failed", "خطای پیش‌بینی نشده‌ای رخ داد.", ErrorType.Unexpected);

        return Result.Success();
    }
}
