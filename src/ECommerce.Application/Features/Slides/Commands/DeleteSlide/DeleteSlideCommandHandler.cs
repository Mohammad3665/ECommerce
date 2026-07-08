namespace ECommerce.Application.Features.Slides.Commands.DeleteSlide;

public class DeleteSlideCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<DeleteSlideCommand, Result>
{
    public async Task<Result> Handle(DeleteSlideCommand request, CancellationToken cancellationToken)
    {
        var slide = await unitOfWork.SlideRepository.GetAsync(
            expression: s => s.Id == request.Id,
            cancellationToken: cancellationToken
        );
        if (slide is null)
        {
            var error = new Error(
                "Slide.NotFound",
                "اسلاید یافت نشد.",
                ErrorType.NotFound
            );
            return Result.Failure(error);
        }

        string imagePathToDelete = slide.ImageUrl;
        var slidesToShift = await unitOfWork.SlideRepository.GetAllWithTrackingAsync(
            expression: s => s.DisplayOrder > slide.DisplayOrder,
            cancellationToken: cancellationToken
        );

        foreach (var slideToShift in slidesToShift)
        {
            slideToShift.DisplayOrder--;
        }
        unitOfWork.SlideRepository.DeletePermanently(slide);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Slide.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result.Failure(error);
        }

        fileService.DeleteFile(imagePathToDelete);
        return Result.Success();
    }
}