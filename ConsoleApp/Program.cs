using System;

namespace ConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            for (var i = 0; i < 20; i++)
            {
                Console.WriteLine(Guid.NewGuid().ToString().ToLower());
            }
        }
    }
}