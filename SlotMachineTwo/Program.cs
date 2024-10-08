namespace SlotMachineTwo;

internal static class SlotMachine
{
    // Символы для барабанов
    private static readonly string[] Symbols = { "🍒", "🍋", "🍉", "⭐", "🔔", "7️⃣" };

    private static readonly Random Random = new Random();

    private static void Main()
    {
        var balance = 100; // Начальный баланс игрока

        Console.WriteLine("Добро пожаловать в слот-машину!");

        while (balance > 0)
        {
            Console.WriteLine($"\nВаш баланс: {balance}");
            Console.Write("Введите ставку (0 для выхода): ");

            if (!int.TryParse(Console.ReadLine(), out int bet) || bet < 0)
            {
                Console.WriteLine("Пожалуйста, введите допустимое число.");
                continue;
            }

            if (bet == 0)
            {
                break; // Игрок решил выйти
            }

            if (bet > balance)
            {
                Console.WriteLine("Недостаточно средств для этой ставки.");
                continue;
            }

            balance -= bet;

            // Вращаем барабаны
            var reels = SpinReels();

            Console.WriteLine($"Результат: {reels[0]} | {reels[1]} | {reels[2]}");

            // Проверяем выигрыш
            var winnings = CheckWinnings(reels, bet);
            balance += winnings;

            if (winnings > 0)
            {
                Console.WriteLine($"Вы выиграли {winnings}!");
            }
            else
            {
                Console.WriteLine("Вы ничего не выиграли.");
            }
        }

        Console.WriteLine($"Игра окончена! Ваш итоговый баланс: {balance}");
    }

    // Функция для вращения барабанов
    private static string[] SpinReels()
    {
        var reels = new string[3];

        for (var i = 0; i < 3; i++)
        {
            reels[i] = Symbols[Random.Next(Symbols.Length)];
        }

        return reels;
    }

    // Проверка выигрыша
    private static int CheckWinnings(string[] reels, int bet)
    {
        if (reels[0] == reels[1] && reels[1] == reels[2])
        {
            if (reels[0] == "7️⃣")
            {
                return bet * 10; // Большой выигрыш за три семерки
            }

            return bet * 5; // Выигрыш за любые три одинаковые символы
        }
        else if (reels[0] == reels[1] || reels[1] == reels[2] || reels[0] == reels[2])
        {
            return bet * 2; // Малый выигрыш за два одинаковых символа
        }

        return 0; // Никакого выигрыша
    }
}