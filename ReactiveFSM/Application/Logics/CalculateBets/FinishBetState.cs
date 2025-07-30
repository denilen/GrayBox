using System;
using ReactiveFSM.Application.BetStateMachine;

namespace ReactiveFSM.Application.Logics.CalculateBets;

/// <summary>
/// finish calculate bet state
/// </summary>
internal sealed class FinishBetState : IFsmBetState
{
    public void Initial(FsmBetState betLogic)
    {
        Console.WriteLine($"FSM not permitted: {betLogic.Guid} cancel -> initial");
    }

    public void Calculate(FsmBetState betLogic)
    {
        Console.WriteLine($"FSM not permitted: {betLogic.Guid} cancel -> calculate");
    }

    public void Cancel(FsmBetState betLogic)
    {
        Console.WriteLine($"FSM not permitted: {betLogic.Guid} finish -> cancel");
    }

    public void Finish(FsmBetState betLogic)
    {
        Console.WriteLine($"FSM already finished {betLogic.Guid}");
    }
}
