namespace SlotMachineOne;

internal static class Program
{
    public static void Main()
    {
        var slotMachine = new SlotMachine();

        slotMachine.Play();
    }
}

internal class SlotMachine
{
    private readonly int _tapesCount = 3;
    private ConsoleColor _machineColor = ConsoleColor.Green;
    public string[] Tapes { get; set; } = { "0", "1", "2", "3", "4", "5", "6", "7", "7", "8", "Q", "K", "A", "B", "C", "D" };

    public void SetMachineColor(ConsoleColor color)
    {
        this._machineColor = color;
    }

    public string[] Play()
    {
        var result = new string[_tapesCount];

        Console.ForegroundColor = _machineColor;
        Console.Clear();
        Console.WriteLine(new string('-', _tapesCount + 2));
        Console.WriteLine("|" + new string('7', _tapesCount) + "|");
        Console.WriteLine(new string('-', _tapesCount + 2));

        var lastCursorPosition = Console.GetCursorPosition();

        Thread.Sleep(1000);

        for (var j = 0; j < _tapesCount; j++)
        {
            var random = new Random();

            for (var i = 0; i < 50; i++)
            {
                Console.SetCursorPosition(j + 1, 1);

                var currentTape = Tapes[random.Next(Tapes.Length)];

                Console.Write(currentTape);

                result[j] = currentTape;

                Thread.Sleep(50);
            }
        }

        Console.SetCursorPosition(lastCursorPosition.Item1, lastCursorPosition.Item2);

        return result;
    }
}
