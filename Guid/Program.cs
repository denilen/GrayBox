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
            var guidNull    = new guidNull();


            Console.WriteLine($"System.Guid.Empty = {guidEmpty}");
            Console.WriteLine($"default(System.Guid) = {guidDefault}");
            Console.WriteLine($"System.Guid.NewGuid() = {guidNew}");
            Console.WriteLine($"System.Guid? = {guidNull.Guid}");

            for (var i = 0; i <= 100; i++)
            {
                Console.WriteLine(System.Guid.NewGuid().ToString());
            }
        }
    }

    public class guidNull
    {
        public System.Guid? Guid { get; set; }
    }
}