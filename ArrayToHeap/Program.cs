using System;
using System.Collections.Generic;
using System.Linq;

namespace ArrayToHeap
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
        public List<Swap> Swapollas { get; set; }

        public void Run(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            GenerateSwaps(input);
            PrintSwaps(Swapollas);
        }

        public void GenerateSwaps(long[] n)
        {
            // add swaps to list of swaps as we transform an array into a min heap
        }

        public void PrintSwaps(List<Swap> swapollas)
        {
            // print the list of swaps generated in GenerateSwaps in the desired format
        }
    }

    public class Swap
    {
        public int i { get; set; }
        public int j { get; set; }

        public Swap(int passed_i, int passed_j)
        {
            i = passed_i;
            j = passed_j;
        }
    }
}
