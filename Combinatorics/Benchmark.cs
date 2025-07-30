using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Combinatorics;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net60)]
[RPlotExporter]
public class CombinatoricsBenchmark
{
    // Test data size
    private readonly int[] _testArray = Enumerable.Repeat(9, 6).ToArray(); // 6 elements with value 9
    private const int Iterations = 3;

    // For progress display
    private static int _totalCombinations = 0;
    private static int _processedCombinations = 0;
    private static readonly object ProgressLock = new object();

    /// <summary>
    ///     Benchmarks the original implementation of generating combinations.
    /// </summary>
    /// <remarks>
    ///     This implementation uses a simple loop to generate all combinations.
    ///     <para>
    ///         The implementation is included as a baseline for comparison with the optimized
    ///         implementation.
    ///     </para>
    /// </remarks>
    [Benchmark(Baseline = true)]
    public void OriginalImplementation()
    {
        for (var i = 0; i < Iterations; i++)
        {
            foreach (var arr in OriginalArrangements(_testArray))
            {
                // Minimal work with the result
                ProcessResult(arr);
            }
        }
    }

    /// <summary>
    /// Benchmarks the optimized implementation of generating combinations.
    /// </summary>
    /// <remarks>
    /// This implementation uses an optimized algorithm to generate all combinations,
    /// improving performance by reducing unnecessary computations and utilizing
    /// efficient data structures.
    /// <para>
    /// The results from this implementation are used to compare against the baseline
    /// to measure improvements in speed and resource usage.
    /// </para>
    /// </remarks>
    [Benchmark]
    public void OptimizedImplementation()
    {
        for (var i = 0; i < Iterations; i++)
        {
            foreach (var arr in OptimizedArrangements(_testArray))
            {
                // Minimal work with the result
                ProcessResult(arr);
            }
        }
    }

    /// <summary>
    /// Benchmarks the parallelized version of the optimized algorithm for generating combinations.
    /// </summary>
    /// <remarks>
    /// This implementation leverages parallel processing to improve performance when generating all combinations.
    /// The use of <c>Parallel.ForEach</c> allows for concurrent processing of combinations, which can lead to
    /// significant performance improvements on multi-core systems.
    /// <para>
    /// The results from this implementation are used to compare against the baseline and other optimized versions
    /// to measure improvements in speed and resource utilization.
    /// </para>
    /// </remarks>
    [Benchmark]
    public void ParallelOptimizedImplementation()
    {
        for (var i = 0; i < Iterations; i++)
        {
            // Using Parallel.ForEach for parallel processing
            var arrangements = OptimizedArrangements(_testArray).ToArray();
            Parallel.ForEach(arrangements, arr => { ProcessResult(arr); });
        }
    }

    // Method for processing the result (simulating a payload)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ProcessResult(Span<int> arr)
    {
        // Simulating useful work with the result
        var sum = 0;

        foreach (var t in arr)
        {
            sum += t;
        }

        // Update progress
        UpdateProgress();
    }

    // Method for updating progress
    private static void UpdateProgress()
    {
        if (_totalCombinations == 0) return;

        lock (ProgressLock)
        {
            _processedCombinations++;

            if (_processedCombinations % 1000 == 0 || _processedCombinations == _totalCombinations)
            {
                var progress = (double)_processedCombinations / _totalCombinations * 100;
                Console.Write($"\rProgress: {progress:0.00}% ({_processedCombinations:N0} / {_totalCombinations:N0})");
            }
        }
    }

    /// <summary>
    ///     Generates all combinations of the given array of maximum values, using a simple iterative approach.
    /// </summary>
    /// <param name="maxValues">Array of maximum values for each position.</param>
    /// <returns>An enumerable sequence of all combinations.</returns>
    /// <remarks>
    ///     This implementation uses a simple loop to generate all combinations.
    ///     <para>
    ///         It is included as a baseline for comparison with the optimized
    ///         implementation.
    ///     </para>
    /// </remarks>
    private static IEnumerable<int[]> OriginalArrangements(int[] maxValues)
    {
        var current = new int[maxValues.Length];
        var length = maxValues.Length;

        yield return (int[])current.Clone();

        while (true)
        {
            var position = length - 1;

            while (position >= 0 && current[position] == maxValues[position])
                position--;

            if (position < 0)
                yield break;

            current[position]++;

            for (var i = position + 1; i < length; i++)
                current[i] = 0;

            yield return (int[])current.Clone();
        }
    }

    /// <summary>
    ///     Generates all combinations of the given array of maximum values, using a more memory-efficient approach.
    /// </summary>
    /// <param name="maxValues">Array of maximum values for each position.</param>
    /// <returns>An enumerable sequence of all combinations.</returns>
    /// <remarks>
    ///     This implementation uses a pool of reusable arrays to reduce memory pressure.
    ///     <para>
    ///         It uses a single loop to generate all combinations.
    ///     </para>
    /// </remarks>
    private static IEnumerable<int[]> OptimizedArrangements(int[] maxValues)
    {
        var length = maxValues.Length;
        var current = ArrayPool<int>.Shared.Rent(length);
        var result = ArrayPool<int>.Shared.Rent(length);
        var max = ArrayPool<int>.Shared.Rent(length);

        try
        {
            // Copy maximum values and initialize the current array
            Array.Copy(maxValues, max, length);
            Array.Clear(current, 0, length);

            // Return the first combination
            Array.Copy(current, result, length);
            yield return result;

            // Generate subsequent combinations
            while (true)
            {
                var position = length - 1;

                // Find the position to increment
                while (position >= 0 && current[position] == max[position])
                    position--;

                if (position < 0)
                    break;

                // Increment and reset the lower bits
                current[position]++;

                for (var i = position + 1; i < length; i++)
                    current[i] = 0;

                // Copy the result and return
                Array.Copy(current, result, length);
                yield return result;
            }
        }
        finally
        {
            // Return arrays to the pool
            ArrayPool<int>.Shared.Return(current);
            ArrayPool<int>.Shared.Return(result);
            ArrayPool<int>.Shared.Return(max);
        }
    }

    // New optimized version using Span<T> and caching
    [Benchmark]
    public void SpanOptimizedImplementation()
    {
        // Using ArrayPool for memory allocation
        var arrayPool = ArrayPool<int>.Shared;
        var current = arrayPool.Rent(_testArray.Length);
        var result = arrayPool.Rent(_testArray.Length);

        try
        {
            // Copy test data
            Array.Copy(_testArray, 0, result, 0, _testArray.Length);

            // Set the total number of combinations to display progress
            // For an array of 100 elements with values from 0 to 9: 10^100 combinations
            // This is an astronomically large number, so we will display progress by the number of iterations
            _totalCombinations = (int)Math.Min(long.MaxValue, (long)Math.Pow(10, Math.Min(10, _testArray.Length)));
            _processedCombinations = 0;

            Console.WriteLine("\nStarting optimized version with Span<T>...");

            for (var i = 0; i < Iterations; i++)
            {
                // Reset the current combination array
                Array.Clear(current, 0, current.Length);

                // Process the first combination
                ProcessResult(current);

                // Generate and process all combinations
                while (true)
                {
                    var position = _testArray.Length - 1;

                    // Find the position to increment
                    while (position >= 0 && current[position] >= result[position])
                        position--;

                    if (position < 0)
                        break;

                    // Increment and reset the lower bits
                    current[position]++;

                    for (var j = position + 1; j < _testArray.Length; j++)
                        current[j] = 0;

                    // Process the combination
                    ProcessResult(current);
                }

                Console.WriteLine($"\nIteration {i + 1} of {Iterations} completed");
            }

            Console.WriteLine("\nOptimized version with Span<T> completed");
        }
        finally
        {
            // Return arrays to the pool
            arrayPool.Return(current);
            arrayPool.Return(result);
        }
    }
}
