using ECommerce.AdminPanel.Models;
using ECommerce.Application.Features.Orders.Queries.GetOrderHistory;
using ECommerce.Application.Features.Orders.Queries.GetOrderById;
using ECommerce.Domain.Enums;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.AdminPanel.Controllers;

public class OrdersController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken ct)
    {
        ViewData["Title"] = "لیست سفارشات";
        var result = await sender.Send(new GetOrderHistoryQuery(), ct);
        var orders = result.IsSuccess ? result.Data.Adapt<List<OrderListViewModel>>() : [];
        return View(orders);
    }

    public async Task<IActionResult> Details(long id, CancellationToken ct)
    {
        ViewData["Title"] = "جزئیات سفارش";
        var result = await sender.Send(new GetOrderByIdQuery(id), ct);
        if (result.IsFailure) return RedirectToAction(nameof(Index));

        var dto = result.Data;
        var model = new OrderDetailViewModel
        {
            OrderId = dto!.OrderId,
            OrderNumber = dto.OrderNumber,
            Status = dto.Status,
            OrderDate = dto.OrderDate,
            SubTotal = dto.SubTotal,
            DiscountAmount = dto.DiscountAmount,
            ShippingCost = dto.ShippingCost,
            TotalAmount = dto.TotalAmount,
            Items = dto.Items.Select(i => new OrderItemViewModel
            {
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                TotalPrice = i.TotalPrice
            }).ToList(),
            CustomerName = dto.Shipping.FullName,
            CustomerPhone = dto.Shipping.PhoneNumber,
            CustomerAddress = dto.Shipping.Address,
            CustomerPostalCode = dto.Shipping.PostalCode
        };
        return View(model);
    }

    public async Task<IActionResult> ChangeStatus(long id, CancellationToken ct)
    {
        ViewData["Title"] = "تغییر وضعیت سفارش";
        var result = await sender.Send(new GetOrderByIdQuery(id), ct);
        var model = result.IsSuccess
            ? new OrderChangeStatusViewModel
            {
                Id = result.Data.OrderId,
                OrderNumber = result.Data.OrderNumber,
                CurrentStatus = result.Data.Status
            }
            : new OrderChangeStatusViewModel { Id = id };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangeStatus(OrderChangeStatusViewModel model, CancellationToken ct)
    {
        // TODO: Send ChangeOrderStatusCommand when available in Application layer
        return RedirectToAction(nameof(Details), new { id = model.Id });
    }
}
