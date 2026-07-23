using ECommerce.Domain.Entities.Common;

namespace ECommerce.Application.Dtos.Comments;

public record AdminCommentsResponseDto(
    Guid Id,
    string Title,
    string Content,
    string UserFullName,
    string UserEmail,
    string? TargetName,
    string TargetType,
    DateTime CreatedAt,
    bool IsApproved,
    DateTime? ApprovedAt
) : IMapFrom<Comment>, IHaveCustomMapping
{
    public static void ConfigureMapping(Mapster.TypeAdapterConfig config)
    {
        config.NewConfig<Comment, AdminCommentsResponseDto>()
            .Map(dest => dest.UserFullName, src => src.User.FullName)
            .Map(dest => dest.UserEmail, src => src.User.Email)
            .Map(dest => dest.TargetName, src => src.ProductId.HasValue ? src.Product!.Name : src.Article!.Title)
            .Map(dest => dest.TargetType, src => src.ProductId.HasValue ? "Product" : "Article");
    }
}