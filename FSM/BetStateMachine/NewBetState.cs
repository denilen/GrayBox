using System;

namespace FSM.BetStateMachine
{
	/// <summary>
	///  new bet state
	/// </summary>
	internal sealed class NewBetState : ICalculateBetState
	{
		public void New(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task already new");
			betLogic.State = new NewBetState();
		}

		public void OnCalculate(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task: new -> calculate");
			betLogic.State = new CalculateBetState();
		}

		public void OnCancel(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task: new -> cancel");
			betLogic.State = new CancelBetState();
		}
	}
}