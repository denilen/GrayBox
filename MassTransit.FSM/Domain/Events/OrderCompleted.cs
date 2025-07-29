namespace MassTransit.FSM.Domain.Events;

public class OrderCompleted
{
    public Guid OrderId { get; }

    public DateTime Timestamp { get; }
}
