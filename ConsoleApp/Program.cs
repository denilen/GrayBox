namespace ConsoleApp;

internal static class Program
{
    private static void Main(string[] args)
    {
        const int test = 1;
        var date = DateTime.Now;
        var dateoffset = DateTimeOffset.Now;

        Console.WriteLine(date.ToString());
        Console.WriteLine(date.ToUniversalTime().ToString());
        Console.WriteLine(dateoffset.ToUniversalTime().ToString());
    }
}