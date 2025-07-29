using MassTransit.FSM.Domain.Events;

namespace MassTransit.FSM.Services;

using MassTransit;

public class OrderService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task SubmitOrder(Guid orderId)
    {
        await _publishEndpoint.Publish<OrderSubmitted>(new
        {
            OrderId = orderId,
            Timestamp = DateTime.UtcNow
        });
    }

    public async Task AcceptOrder(Guid orderId)
    {
        await _publishEndpoint.Publish<OrderAccepted>(new
        {
            OrderId = orderId,
            Timestamp = DateTime.UtcNow
        });
    }

    public async Task CompleteOrder(Guid orderId)
    {
        await _publishEndpoint.Publish<OrderCompleted>(new
        {
            OrderId = orderId,
            Timestamp = DateTime.UtcNow
        });
    }
}

