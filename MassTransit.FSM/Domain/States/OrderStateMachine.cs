using MassTransit;
using MassTransitFSM.Domain.Events;

namespace MassTransitFSM.Domain.States;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State Submitted { get; private set; }
    public State Accepted { get; private set; }
    public State Completed { get; private set; }

    public Event<OrderSubmitted> OrderSubmittedEvent { get; private set; }
    public Event<OrderAccepted> OrderAcceptedEvent { get; private set; }
    public Event<OrderCompleted> OrderCompletedEvent { get; private set; }

    public OrderStateMachine(ILogger<OrderStateMachine> logger)
    {
        InstanceState(x => x.CurrentState);

        Event(() => OrderSubmittedEvent, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => OrderAcceptedEvent, x => x.CorrelateById(m => m.Message.OrderId));
        Event(() => OrderCompletedEvent, x => x.CorrelateById(m => m.Message.OrderId));

        Initially(
            When(OrderSubmittedEvent)
                .Then(context =>
                {
                    logger.LogInformation("Order submitted: {OrderId}", context.Message.OrderId);

                    context.Saga.SubmittedAt = context.Message.Timestamp;
                })
                .TransitionTo(Submitted)
        );

        During(Submitted,
            When(OrderAcceptedEvent)
                .Then(context =>
                {
                    logger.LogInformation("Order accepted: {OrderId}", context.Message.OrderId);

                    context.Saga.AcceptedAt = context.Message.Timestamp;
                })
                .TransitionTo(Accepted)
        );

        During(Accepted,
            When(OrderCompletedEvent)
                .Then(context =>
                {
                    logger.LogInformation("Order completed: {OrderId}", context.Message.OrderId);

                    context.Saga.CompletedAt = context.Message.Timestamp;
                })
                .TransitionTo(Completed)
                .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}
