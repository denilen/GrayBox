namespace RotationOfTheArrow2;

internal static class Program
{
    private static bool _isRunning = true;

    /// <summary>
    /// Runes the execution indicator with a rotating arrow in a separate stream.
    /// </summary>
    private static void StartLoadingIndicator()
    {
        var loadingThread = new Thread(() =>
        {
            char[] spinner = ['|', '/', '-', '\\'];
            var counter = 0;

            while (_isRunning)
            {
                Console.Write($"\r{spinner[counter % spinner.Length]}"); // Display the symbol on the same line

                counter++;
                Thread.Sleep(100); // delay between symbol shifts
            }
            Console.Write("\r "); // Clean the indicator at the end
        })
        {
            IsBackground = true // Stream will end with the program
        };

        loadingThread.Start();
    }

    /// <summary>
    /// Stops the execution indicator.
    /// </summary>
    private static void StopLoadingIndicator()
    {
        Console.Write("\r ");
        _isRunning = false;
    }

    private static void Main()
    {
        Console.WriteLine("Запуск длительной операции...");
        StartLoadingIndicator(); // Launch the indicator

        // Imitation of a long operation
        Thread.Sleep(5000);

        StopLoadingIndicator(); // Stop the indicator

        Console.WriteLine("\nОперация завершена!");
    }
}
