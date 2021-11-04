using System;

namespace Guid
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var guidEmpty   = System.Guid.Empty;
            var guidDefault = default(System.Guid);
            var guidNew     = System.Guid.NewGuid();

            Console.WriteLine($"System.Guid.Empty = {guidEmpty}");
            Console.WriteLine($"default(System.Guid) = {guidDefault}");
            Console.WriteLine($"System.Guid.NewGuid() = {guidNew}");
        }
    }
}