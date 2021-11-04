using System;
using System.Reactive.Linq;

namespace Throttle
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var rand = new Random();
            var source = Observable.Interval(TimeSpan.FromSeconds(rand.Next(1, 2)))
                                   .Do(i => Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: new item: {i}"));
            var sampling = source.Throttle(TimeSpan.FromSeconds(1))
                                 .Do(i => Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: throttle item: {i}"));
            var subscription = sampling.Subscribe();

            Console.ReadLine();

            subscription.Dispose();

            Console.ReadLine();
        }
    }
}