using NAudio.Wave;
using NAudio.Wave.SampleProviders;

internal static class R2D2SoundGenerator
{
    private const int SampleRate = 44100;
    private const float BaseFrequency = 1000f;

    public static void Main()
    {
        Console.WriteLine("Введите текст для преобразования в звуки R2D2_1 (для выхода введите 'exit'):");

        while (true)
        {
            var input = Console.ReadLine();

            if (input?.ToLower() == "exit") break;

            GenerateR2D2Sound(input!);
        }
    }

    private static void GenerateR2D2Sound(string text)
    {
        try
        {
            using var waveOut = new WaveOutEvent();

            foreach (var fade in text.Select(c => BaseFrequency + c * 10)
                         .Select(frequency => new SignalGenerator(SampleRate, 1)
                         {
                             Frequency = frequency,
                             Type = SignalGeneratorType.Sin
                         }).Select(signalGenerator => new FadeInOutSampleProvider(signalGenerator, true)))
            {
                waveOut.Init(fade);
                waveOut.Play();

                // Ждем короткое время между звуками
                Thread.Sleep(100);
                waveOut.Stop();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при генерации звука: {ex.Message}");
        }
    }
}
