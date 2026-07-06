using ECommerce.Application.Common.Validators;
using ECommerce.Domain.Entities.Application.Article;

namespace ECommerce.Application.Features.Articles.Queries.GetPagedArticles;

public class GetPagedArticlesQueryValidator : QueryRequestValidator<GetPagedArticlesQuery, Article>
{
    public GetPagedArticlesQueryValidator() { }
}
