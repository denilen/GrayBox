using System;
using ReactiveFSM.Application.BetStateMachine;

namespace ReactiveFSM.Application.Logics.CalculateBets
{
    /// <summary>
    ///  initial calculate bet state
    /// </summary>
    internal sealed class InitialBetState : IFsmBetState
    {
        public void Initial(FsmBetState betLogic)
        {
            Console.WriteLine($"FSM {betLogic.Guid} already initial");
        }

        public void Calculate(FsmBetState betLogic)
        {
            Console.WriteLine($"FSM: {betLogic.Guid} initial -> calculate");
            betLogic.State = new CalculateBetState();
        }

        public void Cancel(FsmBetState betLogic)
        {
            Console.WriteLine($"FSM: {betLogic.Guid} initial -> cancel");
            betLogic.State = new CancelBetState();
        }

        public void Finish(FsmBetState betLogic)
        {
            Console.WriteLine($"FSM not permitted: {betLogic.Guid} initial -> finish");
        }
    }
}