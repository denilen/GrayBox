namespace MonadicParsersCombinators;

internal static class Program
{
    private static void Main(string[] args)
    {
        var superCalculator = new SuperCalculator();

        var lambda = superCalculator.Evaluate("2+6*2/(7-1)^2");

        Console.WriteLine(lambda.ToString());
        Console.WriteLine(lambda.Compile()());

        // var roman = Calculator.Evaluate(@"(MMMDCCXXIV - MMCCXXIX) * II");
    }
}
