using System;

namespace RefVariable
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var number = 5;
         
            Console.WriteLine($"Число до метода Increment: {number}");
            Increment(ref number);
            Console.WriteLine($"Число после метода Increment: {number}");

            static void Increment(ref int n)
            {
                n++;
                Console.WriteLine($"Число в методе Increment: {n}");
                RefIncrement(ref n);
            }

            static void RefIncrement(ref int n)
            {
                n++;
                Console.WriteLine($"Число в методе RefIncrement: {n}");
            }
        }
    }
}