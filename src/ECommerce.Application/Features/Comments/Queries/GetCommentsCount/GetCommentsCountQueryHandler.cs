namespace ECommerce.Application.Features.Comments.Queries.GetCommentsCount;

public class GetCommentsCountQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCommentsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetCommentsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await unitOfWork.CommentRepository.CountAsync(
            expression: c => c.IsApproved &&
                ((request.ProductId.HasValue && c.ProductId == request.ProductId) ||
                (request.ArticleId.HasValue && c.ArticleId == request.ArticleId)),
            cancellationToken: cancellationToken
        );

        return count;
    }
}