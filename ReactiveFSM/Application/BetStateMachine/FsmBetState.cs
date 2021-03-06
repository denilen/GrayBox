using System;
using ReactiveFSM.Application.Logics.CalculateBets;

namespace ReactiveFSM.Application.BetStateMachine
{
    /// <summary>
    ///  FSM: new -> calculate -> cancel
    /// </summary>
    public class FsmBetState
    {
        public IFsmBetState State { get; set; }
        public Guid         Guid  { get; }

        public FsmBetState()
        {
            Guid  = Guid.NewGuid();
            State = new InitialBetState();
        }

        public void Initial()
        {
            State.Initial(this);
        }

        public void Calculate()
        {
            State.Calculate(this);
        }

        public void Cancel()
        {
            State.Cancel(this);
        }

        public void Finish()
        {
            State.Finish(this);
        }
    }
}