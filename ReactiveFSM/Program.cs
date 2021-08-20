using System;
using System.Threading;
using ReactiveFSM.Application;
using ReactiveFSM.Application.Messages;

namespace ReactiveFSM
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var matchFacade = new LogicsFacade();

            for (var i = 1; i < 10; i++)
            {
                var calculateBets = new CalculateBets { MatchId = i };
                matchFacade.Push(calculateBets);

                Thread.Sleep(10);
            }

            Console.ReadLine();
        }
    }
}
