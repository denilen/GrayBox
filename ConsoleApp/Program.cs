using System.Globalization;

namespace ConsoleApp;

internal static class Program
{
    private static void Main(string[] args)
    {
        var date = DateTime.Now;
        var dateTimeOffset = DateTimeOffset.Now;

        Console.WriteLine(date.ToString(CultureInfo.CurrentCulture));
        Console.WriteLine(date.ToUniversalTime().ToString(CultureInfo.CurrentCulture));
        Console.WriteLine(dateTimeOffset.ToUniversalTime().ToString());
    }
}