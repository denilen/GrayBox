namespace PiGeneratorLeibniz;

internal static class Program
{
    private static void Main(string[] args)
    {
        const int terms = 1000000; // Number of terms in the series
        var pi = 0.0;

        for (var i = 0; i < terms; i++)
        {
            // The number in the numerator
            var numerator = (i % 2 == 0) ? 1.0 : -1.0;

            // The denominator
            double denominator = 2 * i + 1;

            // Add the current term of the series
            pi += numerator / denominator;
        }

        pi *= 4; // Multiply the result by 4 to get pi
        Console.WriteLine($"Approximate value of π: {pi}");
    }
}
