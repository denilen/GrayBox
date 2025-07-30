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
    // Размер тестовых данных
    private readonly int[] _testArray = Enumerable.Repeat(9, 6).ToArray(); // 6 элементов со значением 9
    private const int Iterations = 3;

    // Для отображения прогресса
    private static int _totalCombinations = 0;
    private static int _processedCombinations = 0;
    private static readonly object ProgressLock = new object();

    [Benchmark(Baseline = true)]
    public void OriginalImplementation()
    {
        for (var i = 0; i < Iterations; i++)
        {
            foreach (var arr in OriginalArrangements(_testArray))
            {
                // Минимальная работа с результатом
                ProcessResult(arr);
            }
        }
    }

    [Benchmark]
    public void OptimizedImplementation()
    {
        for (var i = 0; i < Iterations; i++)
        {
            foreach (var arr in OptimizedArrangements(_testArray))
            {
                // Минимальная работа с результатом
                ProcessResult(arr);
            }
        }
    }

    [Benchmark]
    public void ParallelOptimizedImplementation()
    {
        for (var i = 0; i < Iterations; i++)
        {
            // Используем Parallel.ForEach для параллельной обработки
            var arrangements = OptimizedArrangements(_testArray).ToArray();
            Parallel.ForEach(arrangements, arr => { ProcessResult(arr); });
        }
    }

    // Метод для обработки результата (имитация полезной нагрузки)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ProcessResult(Span<int> arr)
    {
        // Имитация полезной работы с результатом
        var sum = 0;

        foreach (var t in arr)
        {
            sum += t;
        }

        // Обновляем прогресс
        UpdateProgress();
    }

    // Метод для обновления прогресса
    private static void UpdateProgress()
    {
        if (_totalCombinations == 0) return;

        lock (ProgressLock)
        {
            _processedCombinations++;

            if (_processedCombinations % 1000 == 0 || _processedCombinations == _totalCombinations)
            {
                var progress = (double)_processedCombinations / _totalCombinations * 100;
                Console.Write($"\rПрогресс: {progress:0.00}% ({_processedCombinations:N0} / {_totalCombinations:N0})");
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

            for (var i = position + 1; i < length; i++)
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
            // Копируем максимальные значения и инициализируем текущий массив
            Array.Copy(maxValues, max, length);
            Array.Clear(current, 0, length);

            // Возвращаем первую комбинацию
            Array.Copy(current, result, length);
            yield return result;

            // Генерируем последующие комбинации
            while (true)
            {
                var position = length - 1;

                // Находим позицию для инкремента
                while (position >= 0 && current[position] == max[position])
                    position--;

                if (position < 0)
                    break;

                // Инкрементируем и сбрасываем младшие разряды
                current[position]++;

                for (var i = position + 1; i < length; i++)
                    current[i] = 0;

                // Копируем результат и возвращаем
                Array.Copy(current, result, length);
                yield return result;
            }
        }
        finally
        {
            // Возвращаем массивы в пул
            ArrayPool<int>.Shared.Return(current);
            ArrayPool<int>.Shared.Return(result);
            ArrayPool<int>.Shared.Return(max);
        }
    }

    // Новая оптимизированная версия с использованием Span<T> и кешированием
    [Benchmark]
    public void SpanOptimizedImplementation()
    {
        // Используем ArrayPool для выделения памяти
        var arrayPool = ArrayPool<int>.Shared;
        var current = arrayPool.Rent(_testArray.Length);
        var result = arrayPool.Rent(_testArray.Length);

        try
        {
            // Копируем тестовые данные
            Array.Copy(_testArray, 0, result, 0, _testArray.Length);

            // Устанавливаем общее количество комбинаций для отображения прогресса
            // Для массива из 100 элементов со значениями от 0 до 9: 10^100 комбинаций
            // Это астрономически большое число, поэтому будем выводить прогресс по количеству итераций
            _totalCombinations = (int)Math.Min(long.MaxValue, (long)Math.Pow(10, Math.Min(10, _testArray.Length)));
            _processedCombinations = 0;

            Console.WriteLine("\nЗапуск оптимизированной версии с Span<T>...");

            for (var i = 0; i < Iterations; i++)
            {
                // Сбрасываем массив текущей комбинации
                Array.Clear(current, 0, current.Length);

                // Обрабатываем первую комбинацию
                ProcessResult(current);

                // Генерируем и обрабатываем все комбинации
                while (true)
                {
                    var position = _testArray.Length - 1;

                    // Находим позицию для инкремента
                    while (position >= 0 && current[position] >= result[position])
                        position--;

                    if (position < 0)
                        break;

                    // Инкрементируем и сбрасываем младшие разряды
                    current[position]++;

                    for (var j = position + 1; j < _testArray.Length; j++)
                        current[j] = 0;

                    // Обрабатываем комбинацию
                    ProcessResult(current);
                }

                Console.WriteLine($"\nИтерация {i + 1} из {Iterations} завершена");
            }

            Console.WriteLine("\nОптимизированная версия с Span<T> завершена");
        }
        finally
        {
            // Возвращаем массивы в пул
            arrayPool.Return(current);
            arrayPool.Return(result);
        }
    }
}
