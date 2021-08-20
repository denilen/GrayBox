using System;

namespace ReactiveStateless.Application.Messages
{
	public interface ILogicMessage
	{
		public Guid LogicId { get; }
	}
}