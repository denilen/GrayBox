using MassTransit.FSM.Services;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.FSM.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("submit/{orderId}")]
    public async Task<IActionResult> Submit(Guid orderId)
    {
        await _orderService.SubmitOrder(orderId);

        return Ok("Order submitted");
    }

    [HttpPost("accept/{orderId}")]
    public async Task<IActionResult> Accept(Guid orderId)
    {
        await _orderService.AcceptOrder(orderId);

        return Ok("Order accepted");
    }

    [HttpPost("complete/{orderId}")]
    public async Task<IActionResult> Complete(Guid orderId)
    {
        await _orderService.CompleteOrder(orderId);

        return Ok("Order completed");
    }
}

