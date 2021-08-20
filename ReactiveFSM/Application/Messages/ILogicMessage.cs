using System;

namespace ReactiveFSM.Application.Messages
{
	public interface ILogicMessage
	{
		public Guid LogicId { get; }
	}
}