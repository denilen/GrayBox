namespace ReactiveFSM.Application.BetStateMachine
{
	public interface IFsmBetState
	{
		void Initial(FsmBetState betLogic);

		void Calculate(FsmBetState betLogic);

		void Cancel(FsmBetState betLogic);

		void Finish(FsmBetState betLogic);
	}
}