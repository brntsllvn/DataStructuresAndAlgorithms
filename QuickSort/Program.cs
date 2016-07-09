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
            
        }

        public void Swap(long[] n, int index1, int index2)
        {

        }

        public void Partition(long[] n)
        {

        }
    }
}
