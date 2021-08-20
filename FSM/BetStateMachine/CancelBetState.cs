using System;

namespace FSM.BetStateMachine
{
	/// <summary>
	/// cancel bet state
	/// </summary>
	internal sealed  class CancelBetState : ICalculateBetState
	{
		public void New(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task not permitted: cancel -> new");
		}

		public void OnCalculate(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task not permitted: cancel -> calculate");
		}

		public void OnCancel(CalculateBetLogic betLogic)
		{
			Console.WriteLine("Task already canceled");
		}
	}
}