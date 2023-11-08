using Isatays.FTGO.OrderService.Api.Models;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Isatays.FTGO.OrderService.Api.Controllers;

/// <summary>
/// Cotroller special for foods
/// </summary>
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/order")]
[ApiVersion("1.0")]
public class OrderController : BaseController
{
	private ILogger<OrderController> _logger;

	public OrderController(ILogger<OrderController> logger)
	{
		_logger = logger;
	}

	[HttpPost("create-order")]
    [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(Order))]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
	{
		var result = await Sender.Send(new CreateOrderCommand(request.Id, request.Name, request.Email));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok();
	}
}
