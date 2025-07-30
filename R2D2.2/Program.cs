using NAudio.Wave;

namespace R2D2_2;

internal static class R2D2SoundGenerator
{
    private static void Main()
    {
        using var waveOut = new WaveOutEvent();

        const int sampleRate = 44100;
        const float duration = 0.1f; // длительность каждого звука в секундах

        while (true)
        {
            // Генерируем случайные параметры для звука
            const float amplitude = 0.25f;
            const int samples = (int)(sampleRate * duration);

            var random = new Random();
            var frequency = random.Next(500, 2500);
            var signal = new float[samples];

            // Создаем звуковой сигнал
            for (var i = 0; i < samples; i++)
            {
                signal[i] = amplitude * (float)Math.Sin((2 * Math.PI * frequency * i) / sampleRate);
            }

            var waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, 1);
            var waveProvider = new BufferedWaveProvider(waveFormat);
            var buffer = new byte[samples * 4];

            Buffer.BlockCopy(signal, 0, buffer, 0, buffer.Length);

            waveProvider.AddSamples(buffer, 0, buffer.Length);
            waveOut.Init(waveProvider);
            waveOut.Play();

            Thread.Sleep((int)(duration * 1000));

            // Случайная пауза между звуками
            Thread.Sleep(random.Next(50, 300));
        }
    }
}
