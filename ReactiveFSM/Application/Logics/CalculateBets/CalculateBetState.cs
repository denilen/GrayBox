using System;
using ReactiveFSM.Application.BetStateMachine;

namespace ReactiveFSM.Application.Logics.CalculateBets
{
	/// <summary>
	///  calculate bet state
	/// </summary>
	internal sealed class CalculateBetState : IFsmBetState
	{
		public void Initial(FsmBetState betLogic)
		{
			Console.WriteLine($"FSM not permitted: {betLogic.Guid} calculate -> initial");
		}

		public void Calculate(FsmBetState betLogic)
		{
			Console.WriteLine($"FSM already calculated {betLogic.Guid}");
		}

		public void Cancel(FsmBetState betLogic)
		{
			Console.WriteLine($"FSM: {betLogic.Guid} calculate -> cancel");
			betLogic.State = new CancelBetState();
		}

		public void Finish(FsmBetState betLogic)
		{
			Console.WriteLine($"FSM: {betLogic.Guid} calculate -> finish");
			betLogic.State = new FinishBetState();
		}
	}
}