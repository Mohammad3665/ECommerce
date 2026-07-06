using ECommerce.Domain.Entities.Common;
using Mapster;

namespace ECommerce.Application.Dtos.Comments;

public record CommentDto(
    Guid Id,
    string Title,
    string Content,
    string UserFullName,
    Guid? ParentCommentId,
    DateTime CreatedAt,
    List<CommentDto> Replies
) : IMapTo<Comment>, IHaveCustomMapping
{
    public static void ConfigureMapping(TypeAdapterConfig config)
    {
        config.NewConfig<Comment, CommentDto>()
            .Map(dest => dest.UserFullName, src => src.User.FullName)
            .Map(dest => dest.Replies, src => new List<CommentDto>());
    }
}