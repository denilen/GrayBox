using System;
using Stateless;

namespace ReactiveStateless.Application.StateLess;

public class CalculateBet
{
    private enum Trigger
    {
        OnInitial,
        OnWaitBet,
        OnApplyBet,
        OnCancel,
        OnFinish
    }

    private enum State
    {
        Initial,
        WaitBet,
        ApplyBet,
        Cancel,
        Finish
    }

    private State _state = State.Initial;

    private readonly StateMachine<State, Trigger>.TriggerWithParameters<long> _setMatchId;

    private readonly string _message;

    public CalculateBet(string message)
    {
        _message = message;
        var machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

        _setMatchId = machine.SetTriggerParameters<long>(Trigger.OnInitial);
        var setLogicId = machine.SetTriggerParameters<long>(Trigger.OnInitial);

        machine.Configure(State.Initial)
            .InternalTransition<long>(setLogicId, (volume, t) => Guid.NewGuid())
            .Permit(Trigger.OnInitial, State.Initial);

        machine.Configure(State.WaitBet)
            .Permit(Trigger.OnWaitBet, State.ApplyBet);

        machine.Configure(State.ApplyBet)
            .Permit(Trigger.OnApplyBet, State.ApplyBet);

        machine.Configure(State.Cancel)
            .Permit(Trigger.OnCancel, State.Cancel);

        machine.Configure(State.Finish)
            .Permit(Trigger.OnFinish, State.Finish);

        machine.OnTransitioned(t =>
            Console.WriteLine($"" +
                              $"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}" +
                              $"({string.Join(", ", t.Parameters)})"));
    }
}
