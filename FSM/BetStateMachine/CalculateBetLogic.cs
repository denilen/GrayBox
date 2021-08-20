using System;
using FSM.Logic;

namespace FSM.BetStateMachine
{
	/// <summary>
	///  FSM: new -> calculate -> cancel
	/// </summary>
	internal sealed class CalculateBetLogic
	{
		public ICalculateBetState State { get; set; }
		public Guid Guid { get; private set; }

		public CalculateBetLogic()
		{
			Guid = Guid.NewGuid();
			State = new NewBetState();
		}

		public void Process(MatchMessage message)
		{
			//if (message is CalculateBetsMessage calculateBetsMessage)
			//  State.CalculateBets(calculateBetsMessage.MatchId);
		}

		public void New()
		{
			State.New(this);
		}

		public void Calculate()
		{
			State.OnCalculate(this);
		}

		public void Cancel()
		{
			State.OnCancel(this);
		}
	}
}