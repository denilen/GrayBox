using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using System.IO;

namespace Combinatorics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создаем конфигурацию с отключенным выводом в консоль
            var config = DefaultConfig.Instance
                .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                .WithOptions(ConfigOptions.DisableLogFile)
                .AddJob(Job.Default
                    .WithWarmupCount(1)
                    .WithIterationCount(3));

            // Запускаем бенчмарк и сохраняем результаты в файл
            var summary = BenchmarkRunner.Run<CombinatoricsBenchmark>(config);
            
            // Сохраняем результаты в файл
            var reportPath = Path.Combine(Directory.GetCurrentDirectory(), "BenchmarkResults.txt");
            File.WriteAllText(reportPath, summary.ToString());
            
            // Выводим путь к файлу с результатами
            System.Console.WriteLine($"Benchmark results saved to: {reportPath}");
        }
    }
}
