using System;
using ReactiveFSM.Application.BetStateMachine;
using ReactiveFSM.Application.Messages;

namespace ReactiveFSM.Application.Logics.CalculateBets;

public class CalculateBetsLogic : LogicBase
{
    public IFsmBetState State { get; set; } = new InitialBetState();

    public override void PushMessage(MatchMessage message)
    {
        Console.WriteLine($"Push message {message.MatchId}");
        throw new NotImplementedException();
    }
}
