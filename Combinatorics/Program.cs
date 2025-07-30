using BenchmarkDotNet.Running;
using System;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;

namespace Combinatorics;

public static class Program
{
    public static void Main(string[] args)
    {
        // Настраиваем конфигурацию бенчмарка
        var config = DefaultConfig.Instance
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddExporter(MarkdownExporter.GitHub)
            .AddExporter(new CsvExporter(CsvSeparator.Comma));

        // Запускаем бенчмарк
        var summary = BenchmarkRunner.Run<CombinatoricsBenchmark>(config);

        // Сохраняем результаты в файл
        const string resultsDir = "BenchmarkResults";

        Directory.CreateDirectory(resultsDir);

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var resultsFile = Path.Combine(resultsDir, $"BenchmarkResults_{timestamp}.md");

        // Сохраняем полный отчёт
        File.WriteAllText(resultsFile, summary.ToString());

        // Сохраняем краткую сводку
        var summaryFile = Path.Combine(resultsDir, "LatestResults.md");
        var reportLines = summary.Reports
            .AsEnumerable()
            .Select(r =>
            {
                var stats = r.GcStats;
                var meanTime = r.ResultStatistics?.Mean / 1000 ?? 0;

                // Получаем информацию о выделенной памяти из метрик
                var memoryMetric = r.Metrics
                    .FirstOrDefault(m => m.Key == "Allocated Memory");

                double allocatedBytes = 0;

                if (memoryMetric.Key != null)
                {
                    // Преобразуем значение метрики в double
                    allocatedBytes = double.TryParse(memoryMetric.Value.ToString(), out var bytes) ? bytes : 0;
                }

                var bytesAllocated = $"{(allocatedBytes / 1024.0):0.00} KB";

                return
                    $"| {r.BenchmarkCase.Descriptor.WorkloadMethod.Name,-30} | {meanTime:0.00} μs | {stats.Gen0Collections,5} | {stats.Gen1Collections,5} | {stats.Gen2Collections,5} | {bytesAllocated,10} |";
            })
            .ToList();

        var summaryContent = $"# Результаты бенчмарков ({DateTime.Now:yyyy-MM-dd HH:mm:ss})\n\n" +
                             "| Метод                            | Время (μs) | Gen 0 | Gen 1 | Gen 2 | Памяти/опер. |\n" +
                             "|----------------------------------|-----------:|------:|------:|------:|-------------:|\n" +
                             string.Join("\n", reportLines);

        File.WriteAllText(summaryFile, summaryContent);

        Console.WriteLine("\nБенчмарки завершены!");
        Console.WriteLine($"Полный отчёт сохранён в: {Path.GetFullPath(resultsFile)}");
        Console.WriteLine($"Краткая сводка: {Path.GetFullPath(summaryFile)}\n");

        // Выводим краткую сводку в консоль
        Console.WriteLine("Краткие результаты:");
        Console.WriteLine(summaryContent);

        // Оставляем консоль открытой для просмотра результатов
        if (!args.Contains("--no-wait"))
        {
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
