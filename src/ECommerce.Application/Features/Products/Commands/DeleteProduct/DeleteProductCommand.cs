namespace ECommerce.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(string Slug) : IRequest<Result>;
