using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace QuickSort
{
    [TestFixture]
    class QuickSortTests
    {
        [Test]
        public void PartitionTests()
        {
            var result = 0;
            result.ShouldBe(12);
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
