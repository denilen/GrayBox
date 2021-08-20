using System;
using ReactiveFSM.Application.Messages;

namespace ReactiveFSM.Application.Logics
{
	public  abstract class LogicBase
	{
		public Guid Id { get;  } = Guid.NewGuid();

		public abstract void PushMessage(MatchMessage message);
	}
}