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

        public void SiftDown(long[] H, long index)
        {
            var size = H.Length - 1;
            var minElementIndex = index;

            var leftChildIndex = GetLeftChildIndex(index);
            if (leftChildIndex <= size && H[leftChildIndex] < H[minElementIndex])
                minElementIndex = leftChildIndex;

            var rightChildIndex = GetRightChildIndex(index);
            if (rightChildIndex <= size && H[rightChildIndex] < H[minElementIndex])
                minElementIndex = rightChildIndex;

            if (index != minElementIndex)
            {
                Swapollas.Add(new Swap(index, minElementIndex));
                SwapElements(H, index, minElementIndex);
                SiftDown(H, minElementIndex);
            }
        }

        public long ExtractMin(long[] H)
        {
            return H[0];
        }

        public void ChangePriority(long[] H, long index, long newPriority)
        {
            var oldPriority = H[index];
            H[index] = newPriority;
            if (newPriority > oldPriority)
                SiftDown(H, index);
            else
                SiftUp(H, index);
        }

        public void SiftUp(long[] H, long index)
        {
            while (index >= 1 && H[GetParentIndexOf(index)] > H[index])
            {
                SwapElements(H, GetParentIndexOf(index), index);
                index = GetParentIndexOf(index);
            }
        }

        public long GetParentIndexOf(long index)
        {
            return (index - 1) / 2;
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
