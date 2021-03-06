using System;

namespace ReactiveFSM.Application.Messages
{
    /// <summary>
    /// Расчитаные кэфы исходов
    /// </summary>
    public class CalculatedBets : MatchMessage, ILogicMessage
    {
        public Guid   LogicId { get; }
        public string Payload { get; set; }
    }
}