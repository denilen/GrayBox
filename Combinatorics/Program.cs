using System;
using System.Collections.Generic;
using System.Linq;

namespace Combinatorics
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            foreach (var arrangement in Arrangements(new[] {1, 2, 3, 2}.ToArray()))
            {
                var arr = string.Join(", ", arrangement);
                Console.WriteLine(arr);
            }
        }

        private static IEnumerable<int[]> Arrangements(this int[] maxValues)
        {
            var a = new int[maxValues.Length];
            var m = maxValues.Length;

            yield return a;

            int j;
            do
            {
                j = m - 1;

                while (j       >= 0
                       && a[j] == maxValues[j])
                    j--;

                if (j < 0)
                    yield break;

                if (a[j] >= maxValues[j])
                    j--;

                a[j]++;

                if (j == m - 1)
                {
                    yield return a;
                }
                else
                {
                    for (var k = j + 1; k < m; k++)
                        a[k] = 0;

                    yield return a;
                }
            } while (j >= 0);
        }
    }
}