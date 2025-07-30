namespace SlotMachine1;

internal static class SlotMachine
{
    private static void Main()
    {
        var rnd = new Random();
        string[] symbols = { "Cherry", "Bell", "Lemon", "Orange", "Star" };

        Console.WriteLine("Welcome to the Slot Machine!");

        while (true)
        {
            Console.WriteLine("\nPress Enter to spin the reels (or type 'exit' to quit):");

            var input = Console.ReadLine();

            if (input?.ToLower() == "exit")
            {
                Console.WriteLine("Thanks for playing! Goodbye.");
                break;
            }

            string[] reels =
            {
                symbols[rnd.Next(symbols.Length)], symbols[rnd.Next(symbols.Length)], symbols[rnd.Next(symbols.Length)]
            };

            Console.WriteLine($"[{reels[0]}] [{reels[1]}] [{reels[2]}]");

            if (reels[0] == reels[1] && reels[1] == reels[2])
            {
                Console.WriteLine("Congratulations! You win!");
            }
            else
            {
                Console.WriteLine("Sorry, you lose. Try again!");
            }
        }
    }
}
