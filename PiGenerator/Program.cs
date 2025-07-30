namespace PiGenerator;

internal static class Program
{
    private static void Main(string[] args)
    {
        const int iterations = 1000000; // Common number of iterations for series-based methods

        Console.WriteLine("Calculating Pi using different methods:");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine($"Leibniz formula:            {CalculatePiLeibniz(iterations)}");
        Console.WriteLine($"Nilakantha series:          {CalculatePiNilakantha(iterations)}");
        Console.WriteLine($"Machin-like formula:        {CalculatePiMachin()}");
        Console.WriteLine(
            $"Gauss-Legendre algorithm:   {CalculatePiGaussLegendre(10)}"); // 10 iterations are enough for high precision
        Console.WriteLine($"Monte Carlo method:         {CalculatePiMonteCarlo(iterations)}");
    }

    // 1. Leibniz formula
    private static double CalculatePiLeibniz(int terms)
    {
        var pi = 0.0;

        for (var i = 0; i < terms; i++)
        {
            var numerator = (i % 2 == 0) ? 1.0 : -1.0;
            double denominator = 2 * i + 1;
            pi += numerator / denominator;
        }

        return pi * 4;
    }

    // 2. Nilakantha series
    private static double CalculatePiNilakantha(int terms)
    {
        var pi = 3.0;
        var sign = 1;

        for (var i = 1; i <= terms; i++)
        {
            double n = 2 * i;
            pi += sign * (4.0 / (n * (n + 1) * (n + 2)));
            sign *= -1;
        }

        return pi;
    }

    // 3. Machin-like formula
    private static double CalculatePiMachin()
    {
        // π = 16 * arctan(1/5) - 4 * arctan(1/239)
        return 16 * Math.Atan(1.0 / 5.0) - 4 * Math.Atan(1.0 / 239.0);
    }

    // 4. Gauss-Legendre algorithm
    private static double CalculatePiGaussLegendre(int iterations)
    {
        var a = 1.0;
        var b = 1.0 / Math.Sqrt(2.0);
        var t = 1.0 / 4.0;
        var p = 1.0;

        for (var i = 0; i < iterations; i++)
        {
            var aNext = (a + b) / 2.0;
            var bNext = Math.Sqrt(a * b);
            var tNext = t - p * Math.Pow(a - aNext, 2.0);
            var pNext = 2.0 * p;

            a = aNext;
            b = bNext;
            t = tNext;
            p = pNext;
        }

        return Math.Pow(a + b, 2.0) / (4.0 * t);
    }

    // 5. Monte Carlo method
    private static double CalculatePiMonteCarlo(int totalPoints)
    {
        var insideCircle = 0;
        var random = new Random();

        for (var i = 0; i < totalPoints; i++)
        {
            // Generate random points in a 1x1 square
            var x = random.NextDouble();
            var y = random.NextDouble();

            // Check if the point is inside the unit circle quadrant
            if (x * x + y * y <= 1)
            {
                insideCircle++;
            }
        }

        // The ratio (insideCircle / totalPoints) approximates π/4
        return 4.0 * insideCircle / totalPoints;
    }
}
