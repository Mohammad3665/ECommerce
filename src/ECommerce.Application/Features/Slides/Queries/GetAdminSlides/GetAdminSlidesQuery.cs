using ECommerce.Application.Dtos.Slides;
using ECommerce.Domain.Common.Filter;
using ECommerce.Domain.Specifications.Common;

namespace ECommerce.Application.Features.Slides.Queries.GetAdminSlides;

public class GetAdminSlidesQuery : QueryRequest, IRequest<Result<Pagination<AdminSlidesResponseDto>>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
}
