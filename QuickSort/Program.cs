using System;
using System.Linq;

namespace QuickSort
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Launcher().Run(args);
        }
    }

    public class Launcher
    {
        public void Run(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
        }

        public void QuickSort(long[] n, int beg, int end)
        {
            if (beg < end)
            {
                var pivot = Partition(n, beg, end);
                QuickSort(n, beg, pivot - 1);
                QuickSort(n, pivot + 1, end);
            }
        }

        public int Partition(long[] n, int beg, int end)
        {
            var pivot = beg;

            for (int j = beg; j < end; j++)
            {
                if (n[j] <= n[end])
                {
                    Swap(n, j, pivot);
                    pivot++;
                }
            }

            Swap(n, pivot, end);

            return pivot;
        }

        public void Swap(long[] n, int indexOfValueToBeOverwritten, int indexOfOverwritingValue)
        {
            var overwrittenValue = n[indexOfValueToBeOverwritten];
            n[indexOfValueToBeOverwritten] = n[indexOfOverwritingValue];
            n[indexOfOverwritingValue] = overwrittenValue;
        }
    }
}
