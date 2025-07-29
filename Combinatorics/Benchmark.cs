using System;
using System.Buffers;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Combinatorics
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net60)]
    [RPlotExporter]
    public class CombinatoricsBenchmark
    {
        private readonly int[] testArray = new[] { 3, 3, 3 }; // Reduced size for faster testing
        private const int Iterations = 5;

        [Benchmark(Baseline = true)]
        public void OriginalImplementation()
        {
            for (int i = 0; i < Iterations; i++)
            {
                foreach (var arr in OriginalArrangements(testArray))
                {
                    // Minimal work with result
                    if (arr.Length == 0) continue;
                }
            }
        }

        [Benchmark]
        public void OptimizedImplementation()
        {
            for (int i = 0; i < Iterations; i++)
            {
                foreach (var arr in OptimizedArrangements(testArray))
                {
                    // Minimal work with result
                    if (arr.Length == 0) continue;
                }
            }
        }

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
                for (int i = position + 1; i < length; i++)
                    current[i] = 0;

                yield return (int[])current.Clone();
            }
        }

        private static IEnumerable<int[]> OptimizedArrangements(int[] maxValues)
        {
            var length = maxValues.Length;
            var current = ArrayPool<int>.Shared.Rent(length);
            var result = ArrayPool<int>.Shared.Rent(length);
            var max = ArrayPool<int>.Shared.Rent(length);

            try
            {
                Array.Copy(maxValues, max, length);
                Array.Clear(current, 0, length);

                Array.Copy(current, result, length);
                yield return result;

                while (true)
                {
                    var position = length - 1;

                    while (position >= 0 && current[position] == max[position])
                        position--;

                    if (position < 0)
                        break;

                    current[position]++;
                    for (int i = position + 1; i < length; i++)
                        current[i] = 0;

                    Array.Copy(current, result, length);
                    yield return result;
                }
            }
            finally
            {
                ArrayPool<int>.Shared.Return(current);
                ArrayPool<int>.Shared.Return(result);
                ArrayPool<int>.Shared.Return(max);
            }
        }
    }
}
