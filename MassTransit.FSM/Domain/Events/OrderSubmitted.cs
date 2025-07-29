namespace MassTransit.FSM.Domain.Events;

public class OrderSubmitted
{
    public Guid OrderId { get; }

    public DateTime Timestamp { get; }
}
