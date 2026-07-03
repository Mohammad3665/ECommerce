using ECommerce.Domain.Common.Result;
using MediatR;

namespace ECommerce.Application.Features.ArticleCategories.Commands.DeleteArticleCategory;

public record DeleteArticleCategoryCommand(string Slug) : IRequest<Result>;
