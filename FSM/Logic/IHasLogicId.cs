using System;

namespace FSM.Logic
{
	public interface IHasLogicId
	{
		public Guid LogicId { get; set; }
	}
}