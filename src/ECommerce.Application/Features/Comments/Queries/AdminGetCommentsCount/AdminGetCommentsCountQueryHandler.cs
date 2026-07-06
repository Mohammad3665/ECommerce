namespace ECommerce.Application.Features.Comments.Queries.AdminGetCommentsCount;

public class AdminGetCommentsCountQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<AdminGetCommentsCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(AdminGetCommentsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await unitOfWork.CommentRepository.CountAsync(
            expression: c =>
                (request.ProductId.HasValue && c.ProductId == request.ProductId) ||
                (request.ArticleId.HasValue && c.ArticleId == request.ArticleId),
            cancellationToken: cancellationToken
        );

        return Result<int>.Success(count);
    }
}