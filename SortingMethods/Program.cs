using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Validators;

namespace SortingMethods;

public static class Program
{
    public static void Main(string[] args)
    {
        // Run benchmarks and get the summary
        var config = DefaultConfig.Instance;
        var summary = BenchmarkRunner.Run<SortingBenchmarks>(config);

        // Print custom summary table
        PrintSummaryTable(summary);
    }

    private static void PrintSummaryTable(Summary summary)
    {
        Console.WriteLine("\n" + new string('=', 100));
        Console.WriteLine("SORTING ALGORITHMS PERFORMANCE COMPARISON");
        Console.WriteLine(new string('-', 100));
        Console.WriteLine("{0,-20} {1,12} {2,15} {3,15} {4,15}",
            "Algorithm",
            "Size",
            "Mean (ns)",
            "Allocated (MB)",
            "GC (0/1/2)");
        Console.WriteLine(new string('-', 100));

        foreach (var report in summary.Reports)
        {
            var benchmark = report.BenchmarkCase;
            var methodName = benchmark.Descriptor.WorkloadMethodDisplayInfo;
            var arraySize = benchmark.Parameters.Items.FirstOrDefault(p => p.Name == "N")?.Value?.ToString() ?? "N/A";

            // Get time metrics
            var stats = report.ResultStatistics;
            var meanTime = stats?.Mean ?? 0;

            // Get memory metrics
            double allocatedMb = 0;
            int gen0 = 0, gen1 = 0, gen2 = 0;

            if (report.Metrics.TryGetValue("Allocated Memory", out var allocated))
            {
                allocatedMb = (double)allocated.Value / 1024 / 1024;
            }
            else if (report.Metrics.TryGetValue("BytesAllocatedPerOperation", out var bytesAllocated))
            {
                allocatedMb = (double)bytesAllocated.Value / 1024 / 1024;
            }

            if (report.Metrics.TryGetValue("Gen 0 Collections", out var gen0col))
                gen0 = (int)gen0col.Value;
            if (report.Metrics.TryGetValue("Gen 1 Collections", out var gen1col))
                gen1 = (int)gen1col.Value;
            if (report.Metrics.TryGetValue("Gen 2 Collections", out var gen2col))
                gen2 = (int)gen2col.Value;

            var gcInfo = $"{gen0}/{gen1}/{gen2}";

            // Output information
            Console.WriteLine("{0,-20} {1,12} {2,15:0.00} {3,15:0.0000} {4,15}",
                methodName,
                arraySize,
                meanTime,
                allocatedMb,
                gcInfo);
        }

        Console.WriteLine(new string('=', 100));
        Console.WriteLine("Note: For detailed benchmark results, check the generated report files");
    }

    private static string FormatBytes(long bytes)
    {
        string[] suffix = { "B", "KB", "MB", "GB", "TB" };
        int i;
        double dblBytes = bytes;

        for (i = 0; i < suffix.Length && bytes >= 1024; i++, bytes /= 1024)
        {
            dblBytes = bytes / 1024.0;
        }

        return $"{dblBytes:0.00} {suffix[i]}";
    }
}

[MemoryDiagnoser]
[RankColumn]
[SimpleJob]
public class SortingBenchmarks
{
    [Params(1000, 5000, 10000)]
    public int N;
    
    private int[] _data;
    private int[] _workingArray;
    
    [GlobalSetup]
    public void Setup()
    {
        var random = new Random(42);
        _data = new int[N];
        _workingArray = new int[N];
        
        for (int i = 0; i < N; i++)
        {
            _data[i] = random.Next();
        }
    }
    
    [IterationSetup]
    public void IterationSetup()
    {
        Array.Copy(_data, _workingArray, N);
    }

    // Методы сортировки будут использовать _workingArray

    // The built-in sort is often highly optimized and serves as a good baseline.
    [Benchmark(Baseline = true)]
    public void ArraySort()
    {
        Array.Sort(_workingArray);
    }

    [Benchmark]
    public void BubbleSort()
    {
        var n = _workingArray.Length;

        for (var i = 0; i < n - 1; i++)

        for (var j = 0; j < n - i - 1; j++)
            if (_workingArray[j] > _workingArray[j + 1])
                Swap(_workingArray, j, j + 1);
    }

    [Benchmark]
    public void SelectionSort()
    {
        var n = _workingArray.Length;

        for (var i = 0; i < n - 1; i++)
        {
            var minIdx = i;

            for (var j = i + 1; j < n; j++)
                if (_workingArray[j] < _workingArray[minIdx])
                    minIdx = j;

            Swap(_workingArray, minIdx, i);
        }
    }

    [Benchmark]
    public void InsertionSort()
    {
        var n = _workingArray.Length;

        for (var i = 1; i < n; ++i)
        {
            var key = _workingArray[i];
            var j = i - 1;

            while (j >= 0 && _workingArray[j] > key)
            {
                _workingArray[j + 1] = _workingArray[j];
                j--;
            }

            _workingArray[j + 1] = key;
        }
    }

    [Benchmark]
    public void QuickSort()
    {
        var array = (int[])_workingArray.Clone();
        QuickSortInternal(array, 0, array.Length - 1);
    }
    
    private void QuickSortInternal(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(array, left, right);
            if (pivot > 1) QuickSortInternal(array, left, pivot - 1);
            if (pivot + 1 < right) QuickSortInternal(array, pivot + 1, right);
        }
    }

    [Benchmark]
    public void MergeSort()
    {
        var array = (int[])_workingArray.Clone();
        MergeSortInternal(array, 0, array.Length - 1);
    }
    
    private void MergeSortInternal(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;
            MergeSortInternal(array, left, middle);
            MergeSortInternal(array, middle + 1, right);
            Merge(array, left, middle, right);
        }
    }

    [Benchmark]
    public void HeapSort()
    {
        var array = (int[])_workingArray.Clone();
        var n = array.Length;

        for (var i = n / 2 - 1; i >= 0; i--)
            Heapify(array, n, i);

        for (var i = n - 1; i > 0; i--)
        {
            Swap(array, 0, i);
            Heapify(array, i, 0);
        }
    }

    [Benchmark]
    public void CountingSort()
    {
        var array = (int[])_workingArray.Clone();
        CountingSortInternal(array);
    }

    [Benchmark]
    public void RadixSort()
    {
        if (_workingArray.Length == 0) return;
        
        var array = (int[])_workingArray.Clone();
        var m = array.Max();

        for (var exp = 1; m / exp > 0; exp *= 10)
            CountSortForRadix(array, exp);
    }

    #region Helper Methods

    private static void Swap(int[] arr, int i, int j)
    {
        (arr[i], arr[j]) = (arr[j], arr[i]);
    }

    private static int Partition(int[] arr, int low, int high)
    {
        var pivot = arr[high];
        var i = low - 1;

        for (var j = low; j < high; j++)
        {
            if (arr[j] >= pivot) continue;
            i++;
            Swap(arr, i, j);
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    private static void Merge(int[] arr, int l, int m, int r)
    {
        var n1 = m - l + 1;
        var n2 = r - m;
        var L = new int[n1];
        var R = new int[n2];

        Array.Copy(arr, l, L, 0, n1);
        Array.Copy(arr, m + 1, R, 0, n2);

        int i = 0, j = 0, k = l;

        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j]) arr[k++] = L[i++];
            else arr[k++] = R[j++];
        }

        while (i < n1) arr[k++] = L[i++];
        while (j < n2) arr[k++] = R[j++];
    }

    private static void Heapify(int[] arr, int n, int i)
    {
        var largest = i;
        var l = 2 * i + 1;
        var r = 2 * i + 2;

        if (l < n && arr[l] > arr[largest]) largest = l;
        if (r < n && arr[r] > arr[largest]) largest = r;
        if (largest == i) return;

        Swap(arr, i, largest);
        Heapify(arr, n, largest);
    }

    private static void CountSortForRadix(int[] arr, int exp)
    {
        var n = arr.Length;
        var output = new int[n];
        var count = new int[10];

        for (var i = 0; i < n; i++) count[(arr[i] / exp) % 10]++;

        for (var i = 1; i < 10; i++) count[i] += count[i - 1];

        for (var i = n - 1; i >= 0; i--)
        {
            output[count[(arr[i] / exp) % 10] - 1] = arr[i];
            count[(arr[i] / exp) % 10]--;
        }

        for (var i = 0; i < n; i++) arr[i] = output[i];
    }

    private static void CountingSortInternal(int[] array)
    {
        if (array == null || array.Length <= 1)
            return;

        // Find min and max values
        int min = array[0];
        int max = array[0];

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < min) min = array[i];
            else if (array[i] > max) max = array[i];
        }

        // If all elements are same, array is already sorted
        if (min == max)
            return;

        // Create and initialize count array
        int range = max - min + 1;
        int[] count = new int[range];

        // Count each element
        for (int i = 0; i < array.Length; i++)
        {
            count[array[i] - min]++;
        }

        // Rebuild the array in sorted order
        int index = 0;
        for (int i = 0; i < range; i++)
        {
            while (count[i] > 0)
            {
                array[index++] = i + min;
                count[i]--;
            }
        }
    }

    #endregion
}
