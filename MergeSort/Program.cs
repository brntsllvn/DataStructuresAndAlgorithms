using System;
using System.Linq;

namespace MergeSort
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
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            MergeSort(input, 0, input.Length - 1);
        }

        public void MergeSort(int[] n, int low, int high)
        {
            if (low < high)
            {
                var mid = low + (high-low) / 2;

                MergeSort(n, low, mid);
                MergeSort(n, mid+1, high);
                Merge(n, low, mid, high);
            }
        }

        private void Merge(int[] n, int low, int mid, int high)
        {
            int bigCounter = low;
           
            var left = new int[mid - low + 1];
            for (int i = 0; bigCounter <= mid; i++)
            {
                left[i] = n[bigCounter];
                bigCounter++;
            }

            var right = new int[high - mid];
            for (int j = 0; bigCounter <= high; j++)
            {
                right[j] = n[bigCounter];
                bigCounter++;
            }

            int leftIncrementer = 0;
            int rightIncrementer = 0;
            for (bigCounter = low; leftIncrementer < left.Length && rightIncrementer < right.Length; bigCounter++)
            {
                if (left[leftIncrementer] < right[rightIncrementer])
                {
                    n[bigCounter] = left[leftIncrementer];
                    leftIncrementer++;
                }
                else
                {
                    n[bigCounter] = right[rightIncrementer];
                    rightIncrementer++;
                }
            }

            for (; leftIncrementer < left.Length; leftIncrementer++, bigCounter++)
            {
                n[bigCounter] = left[leftIncrementer];
            }

            for (; rightIncrementer < right.Length; rightIncrementer++, bigCounter++)
            {
                n[bigCounter] = right[rightIncrementer];
            }
        }
    }
}
