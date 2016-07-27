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

        public Launcher()
        {
            Swapollas = new List<Swap>();
        }

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

        public void SiftDown(long[] H, long parentIndex)
        {
            var size = H.Length - 1;
            var minElementIndex = parentIndex;

            var leftChildIndex = GetLeftChildIndex(parentIndex);
            if (leftChildIndex <= size && H[leftChildIndex] < H[minElementIndex])
                minElementIndex = leftChildIndex;

            var rightChildIndex = GetRightChildIndex(parentIndex);
            if (rightChildIndex <= size && H[rightChildIndex] < H[minElementIndex])
                minElementIndex = rightChildIndex;

            if (parentIndex != minElementIndex)
            {
                SwapElements(H, parentIndex, minElementIndex);
                SiftDown(H, minElementIndex);
            }

        }

        public void SwapElements(long[] H, long index1, long index2)
        {
            var temp = H[index1];
            H[index1] = H[index2];
            H[index2] = temp;
        }

        public long GetLeftChildIndex(long parentIndex)
        {
            return 2 * parentIndex + 1; // "+1" makes the index math 0-based
        }

        public long GetRightChildIndex(long parentIndex)
        {
            return 2 * parentIndex + 2; // "+2" makes the index math 0 - based
        }

        public void PrintSwaps(List<Swap> swapollas)
        {
            // print the list of swaps generated in GenerateSwaps in the desired format
        }
    }

    public class Swap
    {
        public long i { get; set; }
        public long j { get; set; }

        public Swap(long passed_i, long passed_j)
        {
            i = passed_i;
            j = passed_j;
        }
    }
}
