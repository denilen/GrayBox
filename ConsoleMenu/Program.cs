using System.Text;

namespace ConsoleMenu;

internal static class Program
{
    static void Main(string[] args)
    {
        var running = true;

        while (running)
        {
            Console.Clear();
            DisplayMenu();

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowMessage();

                    break;
                case "2":
                    LongRunningProcess();

                    break;
                case "3":
                    running = false;
                    BreakLine("Exit from the application..");

                    break;
                default:
                    BreakLine("The wrong choice. Try it again.");

                    break;
            }

            if (!running) continue;

            BreakLine("Press any key to continue ...");

            Console.ReadKey();
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("====== Main menu ======");
        Console.WriteLine("1. Show the message");
        Console.WriteLine("2. Perform a long process");
        Console.WriteLine("3. Exit");
        Console.WriteLine("=======================");
        Console.Write("Select the option: ");
    }

    private static void ShowMessage()
    {
        const string message = "Hello! This is an example of a formatted conclusion.";

        Console.WriteLine($"\n{message}");
        Console.WriteLine(new string('=', message.Length));
    }

    private static readonly StringBuilder ProgressBuilder = new StringBuilder(100);
    private const int ProgressWidth = 50;
    private const int ProgressUpdateThreshold = 5; // Обновляем каждые 5%

    private static void LongRunningProcess()
    {
        Console.WriteLine("Launching a long process ...\n");
        var progress = 0;
        var lastDrawnProgress = -1;

        while (progress <= 100)
        {
            // Имитация работы
            Thread.Sleep(50);

            // Обновляем прогресс только при достижении порога
            if (progress - lastDrawnProgress >= ProgressUpdateThreshold)
            {
                DrawProgressBar(progress);
                lastDrawnProgress = progress;
            }

            progress++;
        }

        Console.WriteLine("\nThe long process is completed!");
    }

    private static void DrawProgressBar(int progress)
    {
        ProgressBuilder.Clear();
        ProgressBuilder.Append('[');

        var filledWidth = progress * ProgressWidth / 100;
        var emptyWidth = ProgressWidth - filledWidth;

        ProgressBuilder.Append('#', filledWidth);
        ProgressBuilder.Append('-', emptyWidth);
        ProgressBuilder.Append($"] {progress}%");

        Console.CursorLeft = 0;
        Console.Write(ProgressBuilder.ToString());
    }

    private static void BreakLine(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine(new string('-', message.Length));
    }
}
