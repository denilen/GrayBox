using System;
using Stateless;

namespace ReactiveStateless.Application.StateLess
{
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

        private readonly StateMachine<State, Trigger> _machine;
        private          State                        _state = State.Initial;

        private readonly StateMachine<State, Trigger>.TriggerWithParameters<long> _setMatchId;
        private readonly StateMachine<State, Trigger>.TriggerWithParameters<long> _setLogicId;

        private readonly string _message;

        public CalculateBet(string message)
        {
            _message = message;
            _machine = new StateMachine<State, Trigger>(() => _state, s => _state = s);

            _setMatchId = _machine.SetTriggerParameters<long>(Trigger.OnInitial);
            _setLogicId = _machine.SetTriggerParameters<long>(Trigger.OnInitial);

            _machine.Configure(State.Initial)
                    .InternalTransition<long>(_setLogicId, (volume, t) => Guid.NewGuid())
                    .Permit(Trigger.OnInitial, State.Initial);

            _machine.Configure(State.WaitBet)
                    .Permit(Trigger.OnWaitBet, State.ApplyBet);

            _machine.Configure(State.ApplyBet)
                    .Permit(Trigger.OnApplyBet, State.ApplyBet);

            _machine.Configure(State.Cancel)
                    .Permit(Trigger.OnCancel, State.Cancel);

            _machine.Configure(State.Finish)
                    .Permit(Trigger.OnFinish, State.Finish);

            _machine.OnTransitioned(t =>
                Console.WriteLine($""                                                              +
                                  $"OnTransitioned: {t.Source} -> {t.Destination} via {t.Trigger}" +
                                  $"({string.Join(", ", t.Parameters)})"));
        }
    }
}