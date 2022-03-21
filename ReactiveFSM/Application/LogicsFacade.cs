using System;
using System.Collections.Concurrent;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ReactiveFSM.Application.Logics;
using ReactiveFSM.Application.Logics.CalculateBets;
using ReactiveFSM.Application.Messages;

namespace ReactiveFSM.Application
{
    public class LogicsFacade : IDisposable
    {
        //public static
        private static IScheduler Scheduler { get; } = TaskPoolScheduler.Default;

        //public properties
        private readonly Subject<MatchMessage>                 _messageQueue;
        private readonly IDisposable                           _messageQueueDisposer;
        private readonly ConcurrentDictionary<Guid, LogicBase> _logicStore = new();

        //ctor
        public LogicsFacade()
        {
            _messageQueue = new Subject<MatchMessage>();

            var results = _messageQueue.GroupBy(item => item.MatchId)
                                       .SelectMany(group =>
                                           group.ObserveOn(Scheduler)
                                                .Select(item =>
                                                    ProcessMessage(item).ToObservable(Scheduler).Wait())
                                       );

            _messageQueueDisposer = results.Subscribe();
        }

        //public methods
        public void Push(MatchMessage message)
        {
            _messageQueue.OnNext(message);
        }

        public void Dispose()
        {
            _messageQueueDisposer.Dispose();
        }

        private Task<Unit> ProcessMessage(MatchMessage matchMessage)
        {
            LogicBase logic;

            if (matchMessage is ILogicMessage logicMessage)
            {
                logic = _logicStore[logicMessage.LogicId];
            }
            else
            {
                //aka LogicFactory
                logic = matchMessage switch
                {
                    CalculateBets => new CalculateBetsLogic(),
                    _ => throw new InvalidOperationException($"Unknown message type: '{matchMessage.GetType()}'")
                };

                _logicStore[logic.Id] = logic;
            }

            logic.PushMessage(matchMessage);

            return Task.FromResult(Unit.Default);
        }

        //public statics

        //protected (overrides)

        //private

        //private statics
    }
}

/*
* Resistance to refactoring (1)
* Protection against regressions
* Fast feedback
*
* Maintainability
* Value estimate = [0..1] * [0..1] * [0..1] * [0..1]
*/