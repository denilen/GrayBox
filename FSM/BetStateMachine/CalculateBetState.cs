using System;

namespace FSM.BetStateMachine
{
	/// <summary>
	///  calculate bet state
	/// </summary>
	internal sealed class CalculateBetState : ICalculateBetState
	{
		public void New(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task not permitted: calculate -> new");
		}

		public void OnCalculate(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task already calculate");
		}

		public void OnCancel(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task cancel: calculate -> cancel");
			betLogic.State = new CancelBetState();
		}
	}
}