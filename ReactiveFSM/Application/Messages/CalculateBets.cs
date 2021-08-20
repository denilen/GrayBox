namespace ReactiveFSM.Application.Messages
{
	/// <summary>
	/// Команда: посчитать кэфы
	/// </summary>
	public class CalculateBets : MatchMessage
	{
		public string Payload { get; set; }
	}
}