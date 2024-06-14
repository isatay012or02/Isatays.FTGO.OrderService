using Isatays.FTGO.OrderService.Api.Models;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Isatays.FTGO.OrderService.Api.Controllers;

/// <summary>
/// Controller of Order
/// </summary>
[Route("api/v{version:apiVersion}/order")]
[ApiVersion("1.0")]
public class OrderController(ILogger<OrderController> logger) : BaseController
{
	/// <summary>
	/// this endpoint for create an order
	/// </summary>
	[Authorize(Roles = "Moderator")]
	[HttpPost]
    [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(Order))]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
	    var mappedOrder = Mapper.Map<CreateOrderCommand>(request);
		var result = await Sender.Send(mappedOrder);

        // if (result.IsFailed)
        //     return ProblemResponse(result.Error);

        return Ok();
	}
}
