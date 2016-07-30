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
            f0.BuildHeap(input);
            input.ShouldBe(expected);

            var computedSwaps = f0.Swapollas;
            var computedSwapsCount = f0.Swapollas.Count;
            for (int i = 0; i < computedSwapsCount; i++)
            {
                computedSwaps[i].i.ShouldBe(swaps[i].i);
                computedSwaps[i].j.ShouldBe(swaps[i].j);
            }
        }

        private static readonly object[] ListOSwaps =
        {
            new object[] { "A", new long[] { 0 }, new long[] { 0 },
                new List<Swap>() },
            new object[] { "B", new long[] { 0, 1 }, new long[] { 0, 1 },
                new List<Swap>() },
            new object[] { "C", new long[] { 1, 0 }, new long[] { 0, 1 },
                new List<Swap>() { new Swap(0, 1) } },
            new object[] { "D", new long[] { 2, 0, 1, -5, -10 }, new long[] { -10, -5, 1, 2, 0 },
                new List<Swap>() { new Swap(1, 4), new Swap(0, 1), new Swap(1, 3) } },
            new object[] { "E", new long[] { 5, 4, 3, 2, 1 }, new long[] { 1, 2, 3, 5, 4 },
                new List<Swap>() { new Swap(1, 4), new Swap(0, 1), new Swap(1, 3) } }
        };

        [TestCase("A", new long[] { 0 }, 0, new long[] { 0 })]
        [TestCase("B", new long[] { 0, 1 }, 0, new long[] { 0, 1 })]
        [TestCase("C", new long[] { 0, 1 }, 1, new long[] { 0, 1 })]
        [TestCase("D", new long[] { 0, 1, 2 }, 0, new long[] { 0, 1, 2 })]
        [TestCase("E", new long[] { 2, 1, 0 }, 0, new long[] { 0, 1, 2 })]
        [TestCase("F", new long[] { 2, 1, 0 }, 1, new long[] { 2, 1, 0 })]
        [TestCase("G", new long[] { 2, 0, 1, -5, -10 }, 0, new long[] { 0, -10, 1, -5, 2 })]
        [TestCase("H", new long[] { 2, 0, 1, -5, -10 }, 1, new long[] { 2, -10, 1, -5, 0 })]
        public void SiftDown_Works(string caseName, long[] input, long parentIndex, long[] expected)
        {
            var f0 = new Launcher();
            f0.SiftDown(input, parentIndex);
            input.ShouldBe(expected);
        }

        [Test]
        public void Swap_Works()
        {
            var swapper = new Swap(1, 2);
            swapper.i.ShouldBe(1);
            swapper.j.ShouldBe(2);
        }

        [TestCase("A", new long[] { 5 }, 0, new long[] { 5 })]
        [TestCase("B", new long[] { 5, 4 }, 0, new long[] { 5, 4 })]
        [TestCase("C", new long[] { 5, 4 }, 1, new long[] { 4, 5 })]
        [TestCase("D", new long[] { 5, 4, 3 }, 0, new long[] { 5, 4, 3 })]
        [TestCase("D", new long[] { 5, 4, 3 }, 1, new long[] { 4, 5, 3 })]
        [TestCase("E", new long[] { 5, 4, 3 }, 2, new long[] { 3, 4, 5 })]
        [TestCase("F", new long[] { 5, 4, 3, 2, 1 }, 0, new long[] { 5, 4, 3, 2, 1 })]
        [TestCase("G", new long[] { 5, 4, 3, 2, 1 }, 1, new long[] { 4, 5, 3, 2, 1 })]
        [TestCase("H", new long[] { 5, 4, 3, 2, 1 }, 2, new long[] { 3, 4, 5, 2, 1 })]
        [TestCase("I", new long[] { 5, 4, 3, 2, 1 }, 3, new long[] { 2, 5, 3, 4, 1 })]
        [TestCase("J", new long[] { 5, 4, 3, 2, 1 }, 4, new long[] { 1, 5, 3, 2, 4})]
        public void SiftUp_Works(string caseName, long[] input, long index, long[] expected)
        {
            var f0 = new Launcher();
            f0.SiftUp(input, index);
            input.ShouldBe(expected);
        }

        [TestCase("A", new long[] { 5 }, 0, new long[] { 5 })]
        [TestCase("B", new long[] { 5, 4 }, 0, 5, new long[] { 5, 5 })]
        //[TestCase("C", new long[] { 5, 4 }, 1, new long[] { 4, 5 })]
        //[TestCase("D", new long[] { 5, 4, 3 }, 0, new long[] { 5, 4, 3 })]
        //[TestCase("D", new long[] { 5, 4, 3 }, 1, new long[] { 4, 5, 3 })]
        //[TestCase("E", new long[] { 5, 4, 3 }, 2, new long[] { 3, 4, 5 })]
        //[TestCase("F", new long[] { 5, 4, 3, 2, 1 }, 0, new long[] { 5, 4, 3, 2, 1 })]
        //[TestCase("G", new long[] { 5, 4, 3, 2, 1 }, 1, new long[] { 4, 5, 3, 2, 1 })]
        //[TestCase("H", new long[] { 5, 4, 3, 2, 1 }, 2, new long[] { 3, 4, 5, 2, 1 })]
        //[TestCase("I", new long[] { 5, 4, 3, 2, 1 }, 3, new long[] { 2, 5, 3, 4, 1 })]
        //[TestCase("J", new long[] { 5, 4, 3, 2, 1 }, 4, new long[] { 1, 5, 3, 2, 4 })]
        public void ChangePriority_Works(string caseName, long[] input, long index, long newPriority, long[] expected)
        {
            var f0 = new Launcher();
            f0.ChangePriority(input, index, newPriority);
            input.ShouldBe(expected);
        }
    }
}
