using System;
using FSM.BetStateMachine;
using FSM.Identity;

namespace FSM
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var bet = new BetCalculator();

			var calculateBet = new CalculateBetLogic();

			Console.Write($"LogicId [{calculateBet.Guid}] (set calculate) "); calculateBet.Calculate();
			Console.Write($"LogicId [{calculateBet.Guid}] (set cancel) "); calculateBet.Cancel();
			Console.Write($"LogicId [{calculateBet.Guid}] (set new) "); calculateBet.New();
			Console.Write($"LogicId [{calculateBet.Guid}] (set new) "); calculateBet.New();
			Console.Write($"LogicId [{calculateBet.Guid}] (set calculate) "); calculateBet.Calculate();
			Console.Write($"LogicId [{calculateBet.Guid}] (set new) "); calculateBet.New();
			Console.Write($"LogicId [{calculateBet.Guid}] (set calculate) "); calculateBet.Calculate();
			Console.Write($"LogicId [{calculateBet.Guid}] (set cancel) "); calculateBet.Cancel();
			Console.Write($"LogicId [{calculateBet.Guid}] (set cancel) "); calculateBet.Cancel();
			Console.Write($"LogicId [{calculateBet.Guid}] (set calculate) "); calculateBet.Calculate();
			Console.Write($"LogicId [{calculateBet.Guid}] (set new) "); calculateBet.New();
		}
	}
}