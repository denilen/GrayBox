namespace FSM.BetStateMachine
{
	internal interface ICalculateBetState
	{
		void New(CalculateBetLogic betLogic);

		void OnCalculate(CalculateBetLogic betLogic);

		void OnCancel(CalculateBetLogic betLogic);
	}
}