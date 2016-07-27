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
            var dummy = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            BuildHeap(input);
            PrintSwaps(Swapollas);
            Console.Read();
        }

        public void BuildHeap(long[] n)
        {
            var size = n.Length - 1;
            for (int i = size / 2; i >= 0; i--)
                SiftDown(n, i);
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
                Swapollas.Add(new Swap(parentIndex, minElementIndex));
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
            Console.WriteLine(Swapollas.Count);
            foreach (var swap in swapollas)
                Console.WriteLine(swap.i + " " + swap.j);
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
