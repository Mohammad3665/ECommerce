using ECommerce.Application.Features.Payments.Commands.RequestPayment;
using ECommerce.Application.Features.Payments.Commands.VerifyPayment;

namespace ECommerce.Api.Controllers.v1;

public class PaymentController(ISender sender, ILogger<PaymentController> logger) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> RequestPayment([FromBody] RequestPaymentCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }

    [HttpGet]
    public async Task<IActionResult> Verify([FromQuery] VerifyPaymentCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return result.ToActionResult(logger);
    }
}