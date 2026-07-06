namespace ECommerce.Application.Dtos.Comments;

public record CreateCommentRequestDto(
    string Title,
    string Content,
    long? ProductId,
    long? ArticleId,
    Guid? ParentCommentId
);
