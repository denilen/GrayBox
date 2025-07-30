namespace StateMachine2;

public static class StateMachine
{
    enum State
    {
        State1,
        State2,
        State3
    }

    private static State _currentState = State.State1;

    public static void Main()
    {
        Console.WriteLine("Пример конечного автомата на C#");

        while (true)
        {
            Console.WriteLine($"Текущее состояние: {_currentState}");

            Console.WriteLine("Выберите вариант перехода:");
            Console.WriteLine("1. Перейти в следующее состояние");
            Console.WriteLine("2. Сбросить автомат в начальное состояние");
            Console.WriteLine("3. Выйти из программы");

            var choice = int.Parse(Console.ReadLine() ?? string.Empty);

            switch (choice)
            {
                case 1:
                    _currentState = (State)(((int)_currentState + 1) % 3);
                    break;
                case 2:
                    _currentState = State.State1;
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}
