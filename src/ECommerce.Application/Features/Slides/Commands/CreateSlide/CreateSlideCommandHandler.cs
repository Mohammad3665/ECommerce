using ECommerce.Domain.Entities.Application.Slide;
using Mapster;

namespace ECommerce.Application.Features.Slides.Commands.CreateSlide;

public class CreateSlideCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateSlideCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateSlideCommand request, CancellationToken cancellationToken)
    {
        var slidesToShift = await unitOfWork.SlideRepository.GetAllWithTrackingAsync(
            expression: s => s.DisplayOrder >= request.DisplayOrder,
            cancellationToken: cancellationToken
        );

        foreach (var slideToShift in slidesToShift)
        {
            slideToShift.DisplayOrder++;
        }

        var slide = request.Adapt<Slide>();

        await unitOfWork.SlideRepository.AddAsync(slide, cancellationToken);
        var saveResult = await unitOfWork.SaveAsync(cancellationToken);
        if (saveResult.IsFailure)
        {
            var error = new Error(
                "Slide.Failed",
                "خطای پیش‌بینی نشده‌ای رخ داد.",
                ErrorType.Unexpected
            );
            return Result<long>.Failure(error);
        }

        return slide.Id;
    }
}