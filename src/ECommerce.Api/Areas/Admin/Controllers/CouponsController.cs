using ECommerce.Application.Dtos.Coupons;
using ECommerce.Application.Features.Coupons.Commands.CreateCoupon;
using ECommerce.Application.Features.Coupons.Commands.DeleteCoupon;
using ECommerce.Application.Features.Coupons.Commands.EditCoupon;
using ECommerce.Application.Features.Coupons.Commands.ToggleCouponStatus;
using ECommerce.Application.Features.Coupons.Queries.GetAllCoupons;

namespace ECommerce.Api.Areas.Admin.Controllers;

public class CouponsController(ISender sender, ILogger<CouponsController> logger) : AdminBaseController
{
    [HttpGet]
    [HasPermission("coupons.read")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCouponsQuery query, CancellationToken cancellationToken)
    {
        var result = await sender.Send(query, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPost]
    [HasPermission("coupons.create")]
    public async Task<IActionResult> Create([FromBody] CreateCouponRequestDto dto, CancellationToken cancellationToken)
    {
        var command = dto.Adapt<CreateCouponCommand>();
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("coupons.update")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] EditCouponRequestDto dto, CancellationToken cancellationToken)
    {
        var command = new EditCouponCommand(id, dto.Code, dto.Type, dto.Value, dto.MinOrderAmount, dto.UsageLimit, dto.StartDate, dto.EndDate);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpDelete("{id:guid}")]
    [HasPermission("coupons.delete")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCouponCommand(id);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpPut("{id:guid}")]
    [HasPermission("coupons.update")]
    public async Task<IActionResult> ToggleStatus(Guid id, [FromBody] bool isActive, CancellationToken cancellationToken)
    {
        var command = new ToggleCouponStatusCommand(id, isActive);
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}