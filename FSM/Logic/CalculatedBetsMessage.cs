using System;

namespace FSM.Logic
{
    public class CalculatedBetsMessage : MatchMessage, IHasLogicId
    {
        public Guid LogicId { get; set; }
    }
}