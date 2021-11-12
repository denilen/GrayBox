using System;
using System.Diagnostics;

namespace IfElse
{
    internal abstract class Program
    {
        private static void Main(string[] args)
        {
            var stopwatchOne = new Stopwatch();
            var stopwatchTwo = new Stopwatch();
            var person = new Person
            {
                Name = "Performance If..Else example,"
            };

            stopwatchOne.Start(); PrintNameOne(person); stopwatchOne.Stop();
            Console.Write($" code not optimize: {stopwatchOne.ElapsedMilliseconds} ms");
            Console.WriteLine();
            stopwatchTwo.Start(); PrintNameTwo(person); stopwatchTwo.Stop();
            Console.Write($" code is optimize: {stopwatchTwo.ElapsedMilliseconds} ms");
        }

        private static void PrintNameOne(Person p)
        {
            // Bad code example
            if (p != null)
            {
                if (p.Name != null)
                {
                    Console.Write(p.Name);
                }
            }
        }

        private static void PrintNameTwo(Person p)
        {
            // Good code example
            if (p?.Name == null) return;
            Console.Write(p.Name);
        }

        private class Person
        {
            public string Name { get; set; }
        }
    }
}