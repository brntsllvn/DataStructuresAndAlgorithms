using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace QuickSort
{
    [TestFixture]
    class QuickSortTests
    {
        [TestCase("A PARTITION", new long[] { 0 }, 0, 0, new long[] { 0 }, 0)]
        [TestCase("B PARTITION", new long[] { 0, 1 }, 0, 1, new long[] { 0, 1 }, 1)]
        [TestCase("C PARTITION", new long[] { 0, 1, -2 }, 0, 2, new long[] { -2, 1, 0 }, 0)]
        [TestCase("D PARTITION", new long[] { 0, 3, -2, 1}, 0, 3, new long[] { 0, -2, 1, 3 }, 2)]
        [TestCase("E PARTITION", new long[] { 0, 3, -2, -100 }, 0, 3, new long[] { -100, 3, -2, 0}, 0)]
        public void PartitionTests(string caseName, long[] input, int beg, int end, long[] expArray, int expPivot)
        {
            var f0 = new Launcher();
            var pivot = f0.Partition(input, beg, end);
            input.ShouldBe(expArray);
            pivot.ShouldBe(expPivot);
        }

        [TestCase("A SWAP", new long[] { 0 }, 0, 0, new long[] { 0 })]
        [TestCase("B SWAP", new long[] { 0, 1 }, 0, 1, new long[] { 1, 0 })]
        [TestCase("C SWAP", new long[] { 0, 1, 42, -10 }, 1, 3, new long[] { 0, -10, 42, 1 })]
        public void SwapTests(string caseName, long[] input, int index1, int index2, long[] expected)
        {
            var f0 = new Launcher();
            f0.Swap(input, index1, index2);
            input.ShouldBe(expected);
        }

        [TestCase("A QUICKSORT", new long[] { }, new long[] { })]
        [TestCase("B QUICKSORT", new long[] { 0 }, new long[] { 0 })]
        [TestCase("C QUICKSORT", new long[] { 1, 0 }, new long[] { 0, 1 })]
        [TestCase("D QUICKSORT", new long[] { 1, 0, -1}, new long[] { -1, 0, 1 })]
        [TestCase("E QUICKSORT", new long[] { 1, 0, -1, 2 }, new long[] { -1, 0, 1, 2 })]
        [TestCase("F QUICKSORT", new long[] { 1, 0, 42, -1, 2 }, new long[] { -1, 0, 1, 2, 42 })]
        public void QuickSortTestCases(string caseName, long[] input, long[] expected)
        {
            var f0 = new Launcher();
            var inputMaxIndex = input.Length - 1;
            f0.QuickSort(input, 0, inputMaxIndex);
            input.ShouldBe(expected);

            Should.CompleteIn(
                () => f0.QuickSort(input, 0, inputMaxIndex), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
