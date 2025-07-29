using System;
using System.Threading;

class Program
{
    static void Main()
    {
        char[] spinner = new char[] { '\u2192', '\u2193', '\u2190', '\u2191' }; // → - ↑ это Unicode стрелки
        int index = 0;

        Console.WriteLine("Процесс выполняется, пожалуйста подождите...");

        while (true)
        {
            // Позиционируем курсор на фиксированное место
            Console.SetCursorPosition(0, 1);

            // Выводим текущую стрелку
            Console.Write(spinner[index]);

            // Переход к следующей стрелке
            index = (index + 1) % spinner.Length;

            // Задержка для имитации движения
            Thread.Sleep(200);
        }
    }
}
