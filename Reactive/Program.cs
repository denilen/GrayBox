using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Reactive
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var dialogSubject = new Subject<TaskCalculate>();

			//var scheduler = ThreadPoolScheduler.Instance;
			var scheduler = TaskPoolScheduler.Default;

			var results = dialogSubject.GroupBy(item => item.MatchId)
				.SelectMany(group =>
					group.ObserveOn(scheduler)
					.Select(item => Process(item).ToObservable(scheduler).Wait())
				);

			var groupDisposer = results.Subscribe();

			using (groupDisposer)
			{
				for (var i = 0; i < 40; i++)
				{
					dialogSubject.OnNext(Foo(1, $"1.{i}"));
					dialogSubject.OnNext(Foo(2, $"2.{i}"));
				}

				Console.ReadLine();
			}

			static async Task<Unit> Process(TaskCalculate t)
			{
				Console.WriteLine($"Started: {t.Message}  ThreadId: {Thread.CurrentThread.ManagedThreadId}");

				await Task.Delay(1000);

				Console.WriteLine($"Finished: {t.Message}");
				// Console.WriteLine($"Finished: {t.Message}  ThreadId: {Thread.CurrentThread.ManagedThreadId}");

				return Unit.Default;
			}
		}

		private static TaskCalculate Foo(long matchId, string message)
		{
			var calc = new TaskCalculate
			{
				MatchId = matchId,
				LogicId = null,
				Message = message
			};

			return calc;
		}
	}
}