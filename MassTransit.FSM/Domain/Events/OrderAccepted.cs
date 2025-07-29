namespace MassTransit.FSM.Domain.Events;

public class OrderAccepted
{
    public Guid OrderId { get; }

    public DateTime Timestamp { get; }
}
