using ArrayToHeap;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace LauncherTemplate
{
    [TestFixture]
    class ArrayToHeapTests
    {
        [Test, TestCaseSource(nameof(ListOSwaps))]
        public void Array_To_Heap(string caseName, long[] input, long[] expected, List<Swap> swaps)
        {
            var f0 = new Launcher();
            f0.GenerateSwaps(input);
            input.ShouldBe(expected);

            f0.Swapollas.ShouldBe(swaps);
        }

        private static readonly object[] ListOSwaps =
        {
            new object[] { "A", new long[] { 0 }, new long[] { 0 }, new List<Swap>() },
            new object[] { "B", new long[] { 1, 0 }, new long[] { 0, 1 }, new List<Swap>() { new Swap(1, 0) } }
        };

        [Test]
        public void Swap_Works()
        {
            var swapper = new Swap(1, 2);
            swapper.i.ShouldBe(1);
            swapper.j.ShouldBe(2);
        }
    }
}
