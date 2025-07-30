using System.Numerics;
using ExtendedNumerics;

namespace PiGenerator;

internal static class Program
{
    // Set the precision for BigDecimal calculations
    private const int Precision = 100;

    private static void Main(string[] args)
    {
        const int iterations = 1000000;
        const int chudnovskyIterations = 8; // Each iteration adds ~14 digits
        const int bbpIterations = 100;

        Console.WriteLine("Calculating Pi using different methods:");
        Console.WriteLine("----------------------------------------");

        Console.WriteLine($"Leibniz formula:            {CalculatePiLeibniz(iterations)}");
        Console.WriteLine($"Nilakantha series:          {CalculatePiNilakantha(iterations)}");
        Console.WriteLine($"Machin-like formula:        {CalculatePiMachin()}");
        Console.WriteLine($"Gauss-Legendre algorithm:   {CalculatePiGaussLegendre(10)}");
        Console.WriteLine($"Monte Carlo method:         {CalculatePiMonteCarlo(iterations)}");
        Console.WriteLine($"Wallis Product:             {CalculatePiWallis(iterations)}");
        Console.WriteLine($"Chudnovsky algorithm:       {CalculatePiChudnovsky(chudnovskyIterations)}");
        Console.WriteLine($"BBP formula:                {CalculatePiBbp(bbpIterations)}");
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

    // 6. Wallis Product
    private static double CalculatePiWallis(int iterations)
    {
        var pi = 1.0;

        for (var i = 1; i <= iterations; i++)
        {
            var n = 2.0 * i;

            pi *= (n / (n - 1)) * (n / (n + 1));
        }

        return pi * 2;
    }

    // 7. Chudnovsky Algorithm (with high precision)
    private static BigDecimal CalculatePiChudnovsky(int iterations)
    {
        BigDecimal.Precision = Precision + 10; // Use higher precision for intermediate calculations

        BigDecimal sum = 0;
        BigDecimal c = 640320;

        var c3 = BigDecimal.Pow(c, 3);

        for (var k = 0; k < iterations; k++)
        {
            var sixKFactorial = Factorial(6 * k);
            var threeKFactorial = Factorial(3 * k);
            var kFactorial = Factorial(k);

            var l = 13591409 + (new BigDecimal(545140134) * k);
            var mNumerator = new BigDecimal(sixKFactorial);
            var mDenominator = new BigDecimal(threeKFactorial) * BigDecimal.Pow(new BigDecimal(kFactorial), 3);
            var m = mNumerator / mDenominator;

            var term = m * l;

            if (k % 2 == 1) term *= -1;

            sum += term / BigDecimal.Pow(c, 3 * k);
        }

        var twelveOver = 12 / Sqrt(c3, Precision);
        var piInverse = twelveOver * sum;
        var pi = 1 / piInverse;

        BigDecimal.Precision = Precision; // Reset to desired output precision

        return BigDecimal.Round(pi, Precision);
    }

    // 8. Bailey-Borwein-Plouffe (BBP) formula (with high precision)
    private static BigDecimal CalculatePiBbp(int iterations)
    {
        BigDecimal pi = 0;

        for (var k = 0; k <= iterations; k++)
        {
            var term1 = new BigDecimal(4) / (8 * k + 1);
            var term2 = new BigDecimal(2) / (8 * k + 4);
            var term3 = new BigDecimal(1) / (8 * k + 5);
            var term4 = new BigDecimal(1) / (8 * k + 6);

            var oneOver16K = BigDecimal.One / BigDecimal.Pow(16, k);

            pi += oneOver16K * (term1 - term2 - term3 - term4);
        }

        return BigDecimal.Round(pi, Precision);
    }

    // Helper for Chudnovsky algorithm
    private static BigInteger Factorial(int n)
    {
        if (n == 0) return 1;

        BigInteger result = 1;

        for (var i = 1; i <= n; i++)
        {
            result *= i;
        }

        return result;
    }

    // Helper for square root using Newton's method
    private static BigDecimal Sqrt(BigDecimal n, int precision)
    {
        if (n < 0) throw new ArgumentException("Cannot calculate the square root of a negative number.");
        if (n == 0) return 0;

        // Set precision for the calculation context
        BigDecimal.Precision = precision + 5; // Use slightly higher precision for calculation

        var x = new BigDecimal(Math.Sqrt((double)n)); // Initial guess from double
        var lastX = BigDecimal.Zero;

        for (int i = 0; i < 100; i++) // 100 iterations is more than enough
        {
            lastX = x;
            x = (x + n / x) / 2;

            if (x == lastX)
            {
                break;
            }
        }

        BigDecimal.Precision = precision; // Reset precision

        return BigDecimal.Round(x, precision);
    }
}
