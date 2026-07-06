using ECommerce.Domain.Entities.Application.Slide;
using Mapster;

namespace ECommerce.Application.Features.Slides.Commands.EditSlide;

public class EditSlideCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<EditSlideCommand, Result>
{
    public async Task<Result> Handle(EditSlideCommand request, CancellationToken cancellationToken)
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

        if (slide.DisplayOrder != request.DisplayOrder)
        {
            List<Slide> affectedSlides;
            if (request.DisplayOrder < slide.DisplayOrder)
            {
                affectedSlides = (await unitOfWork.SlideRepository.GetAllAsync(
                    expression: s => s.Id != request.Id && s.DisplayOrder >= request.DisplayOrder && s.DisplayOrder < slide.DisplayOrder,
                    cancellationToken: cancellationToken
                )).ToList();

                foreach (var affectedSlide in affectedSlides) affectedSlide.DisplayOrder++;
            }
            else
            {
                affectedSlides = (await unitOfWork.SlideRepository.GetAllAsync(
                    expression: s => s.Id != request.Id && s.DisplayOrder <= request.DisplayOrder && s.DisplayOrder > slide.DisplayOrder,
                    cancellationToken: cancellationToken
                )).ToList();

                foreach (var affectedSlide in affectedSlides) affectedSlide.DisplayOrder--;
            }
            slide.DisplayOrder = request.DisplayOrder;
        }

        string oldImagePath = slide.ImageUrl;
        bool hasNewImage = request.ImageUrl != null;

        var config = new TypeAdapterConfig();
        config.NewConfig<EditSlideCommand, Slide>()
            .IgnoreNullValues(true)
            .Ignore(dest => dest.ImageUrl);

        request.Adapt(slide, config);

        if (hasNewImage)
        {
            slide.ImageUrl = request.ImageUrl!;
        }

        unitOfWork.SlideRepository.Update(slide);
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

        if (hasNewImage && !string.IsNullOrEmpty(oldImagePath))
        {
            fileService.DeleteFile(oldImagePath);
        }

        return Result.Success();
    }
}