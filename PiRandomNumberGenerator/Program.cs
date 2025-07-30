namespace PiRandomNumberGenerator;

internal static class PiRandomNumberGenerator
{
    private static void Main()
    {
        const string piDigits = "31415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679";
        var random = new Random();

        Console.WriteLine("Random number generator based on the number PI");

        Console.WriteLine("Generated random number:");

        for (var i = 0; i < 5; i++)
        {
            var index = random.Next(0, piDigits.Length);

            Console.Write(piDigits[index]);
        }

        Console.WriteLine();
    }
}
