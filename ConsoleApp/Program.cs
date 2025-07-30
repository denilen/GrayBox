using System.Globalization;

namespace ConsoleApp;

internal static class Program
{
    private static void Main(string[] args)
    {
        var date = DateTime.Now;
        var dateTimeOffset = DateTimeOffset.Now;

        DateOnly defaultDate = default;
        var d = new DateOnly(2023, 1, 1);
        var dd = new DateOnly(2024, 1, 1);

        var onYear = defaultDate.AddYears(2025);

        var resultDateCalulation = onYear > defaultDate;
        var r = d < dd;

        Console.WriteLine(resultDateCalulation);
        Console.WriteLine(r);

        // Console.WriteLine(date.ToString(CultureInfo.CurrentCulture));
        // Console.WriteLine(date.ToUniversalTime().ToString(CultureInfo.CurrentCulture));
        // Console.WriteLine(dateTimeOffset.ToUniversalTime().ToString());
        //
        //
        // Console.WriteLine("1) Split a string delimited by characters:\n");
        //
        // const string s1 = "";
        // var charSeparators = new char[] { ',' };
        // string[] result;
        //
        // Console.WriteLine($"The original string is: \"{s1}\".");
        // Console.WriteLine($"The delimiter character is: '{charSeparators[0]}'.\n");
        //
        // Console.WriteLine("1a) Return all elements:");
        // result = s1.Split(charSeparators, StringSplitOptions.None);
        // Show(result);
        //
        // Console.WriteLine("1b) Return all elements with whitespace trimmed:");
        // result = s1.Split(charSeparators, StringSplitOptions.TrimEntries);
        // Show(result);
        //
        // Console.WriteLine("1c) Return all non-empty elements:");
        // result = s1.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
        // Show(result);
        //
        // Console.WriteLine("1d) Return all non-whitespace elements with whitespace trimmed:");
        // result = s1.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        // Show(result);
        //
        //
        // Console.WriteLine("1e) Split into only two elements:");
        // result = s1.Split(charSeparators, 2, StringSplitOptions.None);
        // Show(result);
        //
        // Console.WriteLine("1f) Split into only two elements with whitespace trimmed:");
        // result = s1.Split(charSeparators, 2, StringSplitOptions.TrimEntries);
        // Show(result);
        //
        // Console.WriteLine("1g) Split into only two non-empty elements:");
        // result = s1.Split(charSeparators, 2, StringSplitOptions.RemoveEmptyEntries);
        // Show(result);
        //
        // Console.WriteLine("1h) Split into only two non-whitespace elements with whitespace trimmed:");
        // result = s1.Split(charSeparators, 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        // Show(result);
        //
        // Console.WriteLine("2) Split a string delimited by another string:\n");
        //
        // const string s2 = "[stop]" +
        //                   "ONE[stop] [stop]" +
        //                   "TWO  [stop][stop]  [stop]" +
        //                   "THREE[stop][stop]  ";
        // var stringSeparators = new string[] { "[stop]" };
        //
        // Console.WriteLine($"The original string is: \"{s2}\".");
        // Console.WriteLine($"The delimiter string is: \"{stringSeparators[0]}\".\n");
        //
        // Console.WriteLine("2a) Return all elements:");
        // result = s2.Split(stringSeparators, StringSplitOptions.None);
        // Show(result);
        //
        // Console.WriteLine("2b) Return all elements with whitespace trimmed:");
        // result = s2.Split(stringSeparators, StringSplitOptions.TrimEntries);
        // Show(result);
        //
        // Console.WriteLine("2c) Return all non-empty elements:");
        // result = s2.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
        // Show(result);
        //
        // Console.WriteLine("2d) Return all non-whitespace elements with whitespace trimmed:");
        // result = s2.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        // Show(result);
        //
        // Console.WriteLine("2e) Split into only two elements:");
        // result = s2.Split(stringSeparators, 2, StringSplitOptions.None);
        // Show(result);
        //
        // Console.WriteLine("2f) Split into only two elements with whitespace trimmed:");
        // result = s2.Split(stringSeparators, 2, StringSplitOptions.TrimEntries);
        // Show(result);
        //
        // Console.WriteLine("2g) Split into only two non-empty elements:");
        // result = s2.Split(stringSeparators, 2, StringSplitOptions.RemoveEmptyEntries);
        // Show(result);
        //
        // Console.WriteLine("2h) Split into only two non-whitespace elements with whitespace trimmed:");
        // result = s2.Split(stringSeparators, 2, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        // Show(result);



        return;

        void Show(string[] entries)
        {
            Console.WriteLine($"The return value contains these {entries.Length} elements:");
            foreach (var entry in entries)
            {
                Console.Write($"<{entry}>");
            }

            Console.Write("\n\n");
        }
    }
}
