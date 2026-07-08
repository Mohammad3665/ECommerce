using ECommerce.Application.Features.Cart.Commands.AddToCart;
using ECommerce.Application.Features.Cart.Commands.ClearCart;
using ECommerce.Application.Features.Cart.Commands.RemoveFromCart;
using ECommerce.Application.Features.Cart.Commands.UpdateCartItemQuantity;
using ECommerce.Application.Features.Cart.Queries.GetCart;
using ECommerce.Application.Features.Orders.Commands.CreateOrder;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Application.Features.Orders.Queries.GetOrderHistory;

namespace ECommerce.Api.Controllers.v1;

public class CartController(ISender sender, ILogger<CartController> logger) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
    {
        var query = new GetCartQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCartItemQuantity([FromBody] UpdateCartItemQuantityCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> RemoveFromCart(long id, CancellationToken cancellationToken)
    {
        var command = new RemoveFromCartCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
    {
        var command = new ClearCartCommand();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderHistory(CancellationToken cancellationToken)
    {
        var query = new GetOrderHistoryQuery();
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetOrderById(long id, CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(id);
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }
}