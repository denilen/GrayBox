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
        // Configure the benchmark
        var config = DefaultConfig.Instance
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .AddExporter(MarkdownExporter.GitHub)
            .AddExporter(new CsvExporter(CsvSeparator.Comma));

        // Run the benchmark
        var summary = BenchmarkRunner.Run<CombinatoricsBenchmark>(config);

        // Save the results to a file
        const string resultsDir = "BenchmarkResults";

        Directory.CreateDirectory(resultsDir);

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var resultsFile = Path.Combine(resultsDir, $"BenchmarkResults_{timestamp}.md");

        // Save the full report
        File.WriteAllText(resultsFile, summary.ToString());

        // Save the summary
        var summaryFile = Path.Combine(resultsDir, "LatestResults.md");
        var reportLines = summary.Reports
            .AsEnumerable()
            .Select(r =>
            {
                var stats = r.GcStats;
                var meanTime = r.ResultStatistics?.Mean / 1000 ?? 0;

                // Get allocated memory information from metrics
                var memoryMetric = r.Metrics
                    .FirstOrDefault(m => m.Key == "Allocated Memory");

                double allocatedBytes = 0;

                if (memoryMetric.Key != null)
                {
                    // Convert the metric value to double
                    allocatedBytes = double.TryParse(memoryMetric.Value.ToString(), out var bytes) ? bytes : 0;
                }

                var bytesAllocated = $"{(allocatedBytes / 1024.0):0.00} KB";

                return
                    $"| {r.BenchmarkCase.Descriptor.WorkloadMethod.Name,-30} | {meanTime:0.00} μs | {stats.Gen0Collections,5} | {stats.Gen1Collections,5} | {stats.Gen2Collections,5} | {bytesAllocated,10} |";
            })
            .ToList();

        var summaryContent = $"# Benchmark Results ({DateTime.Now:yyyy-MM-dd HH:mm:ss})\n\n" +
                             "| Method                           | Time (μs) | Gen 0 | Gen 1 | Gen 2 | Allocated/Op |\n" +
                             "|----------------------------------|-----------:|------:|------:|------:|-------------:|\n" +
                             string.Join("\n", reportLines);

        File.WriteAllText(summaryFile, summaryContent);

        Console.WriteLine("\nBenchmarks completed!");
        Console.WriteLine($"Full report saved to: {Path.GetFullPath(resultsFile)}");
        Console.WriteLine($"Summary: {Path.GetFullPath(summaryFile)}\n");

        // Print the summary to the console
        Console.WriteLine("Summary Results:");
        Console.WriteLine(summaryContent);

        // Keep the console open to view the results
        if (!args.Contains("--no-wait"))
        {
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
