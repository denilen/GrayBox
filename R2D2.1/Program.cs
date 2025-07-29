using System;
using System.Threading;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

public class R2D2SoundGenerator
{
    private const int SampleRate = 44100;
    private const float BaseFrequency = 1000f;

    public static void Main()
    {
        Console.WriteLine("Введите текст для преобразования в звуки R2D2_1 (для выхода введите 'exit'):");

        while (true)
        {
            string input = Console.ReadLine();
            if (input.ToLower() == "exit") break;

            GenerateR2D2Sound(input);
        }
    }

    private static void GenerateR2D2Sound(string text)
    {
        try
        {
            using (var waveOut = new WaveOutEvent())
            {
                foreach (char c in text)
                {
                    // Генерируем частоту на основе символа
                    float frequency = BaseFrequency + (c * 10);

                    // Создаем генератор сигнала
                    var signalGenerator = new SignalGenerator(SampleRate, 1)
                    {
                        Frequency = frequency,
                        Type = SignalGeneratorType.Sin
                    };

                    // Добавляем эффект затухания
                    var fade = new FadeInOutSampleProvider(signalGenerator, true);

                    waveOut.Init(fade);
                    waveOut.Play();

                    // Ждем короткое время между звуками
                    Thread.Sleep(100);
                    waveOut.Stop();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при генерации звука: {ex.Message}");
        }
    }
}

/*
Анализ кода:

1. Зависимости:
   - Программа использует библиотеку NAudio для работы со звуком
   - Требуется установка NuGet пакета NAudio

2. Основные компоненты:
   - SampleRate = 44100 Hz - стандартная частота дискретизации для аудио
   - BaseFrequency = 1000 Hz - базовая частота для генерации звуков

3. Принцип работы:
   - Программа читает текст из консоли
   - Для каждого символа генерируется уникальный звук
   - Частота звука зависит от ASCII-кода символа
   - Используется синусоидальный сигнал для более "электронного" звучания

4. Особенности реализации:
   - Применяется эффект затухания для плавности звуков
   - Между звуками добавлены паузы для разборчивости
   - Реализована обработка ошибок

5. Ограничения:
   - Требует поддержки аудиоустройств
   - Может потребоваться настройка прав доступа к аудио на MacOS

6. Возможные улучшения:
   - Добавление различных звуковых эффектов
   - Настройка параметров генерации через конфигурацию
   - Сохранение сгенерированного звука в файл
*/
