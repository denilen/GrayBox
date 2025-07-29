namespace MassTransitFSM.Domain.States;

using MassTransit;

/// <summary>
/// Represents the state of an order in the saga state machine.
/// </summary>
public class OrderState : SagaStateMachineInstance
{
    /// <summary>
    /// Gets or sets the unique identifier for the order saga instance.
    /// </summary>
    public Guid CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets the current state of the order in the saga.
    /// </summary>
    public string CurrentState { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was submitted.
    /// </summary>
    public DateTime? SubmittedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was accepted.
    /// </summary>
    public DateTime? AcceptedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the order was completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }
}
