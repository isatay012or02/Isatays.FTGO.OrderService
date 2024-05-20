using Isatays.FTGO.OrderService.Api.Models;
using Isatays.FTGO.OrderService.Core.Entities;
using Isatays.FTGO.OrderService.Core.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using AutoMapper;

namespace Isatays.FTGO.OrderService.Api.Controllers;

/// <summary>
/// Cotroller special for foods
/// </summary>
[Route("api/v{version:apiVersion}/order")]
[ApiVersion("1.0")]
public class OrderController : BaseController
{
	private readonly ILogger<OrderController> _logger;
	private readonly IMapper _mapper;

	public OrderController(ILogger<OrderController> logger, IMapper mapper)
	{
		_logger = logger;
		_mapper = mapper;
	}

	[HttpPost("create-order")]
    [ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(Order))]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
	    var mappedOrder = _mapper.Map<CreateOrderCommand>(request);
		var result = await Sender.Send(mappedOrder);

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok();
	}
}
