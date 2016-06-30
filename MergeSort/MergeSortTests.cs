using NUnit.Framework;
using Shouldly;

namespace MergeSort
{
    [TestFixture]
    class MergeSortTests
    {
        [TestCase("A", new[] { 0 },    new[] { 0 })]
        [TestCase("B", new[] { 0, 1 }, new[] { 0, 1 })]
        [TestCase("C", new[] { 1, 0 }, new[] { 0, 1 })]
        [TestCase("D", new[] { 0, 1, 3, 2 }, new[] { 0, 1, 2, 3 })]
        [TestCase("E", new[] { 1, 0, 3, 2 }, new[] { 0, 1, 2, 3 })]
        [TestCase("F", new[] { 1, 0, 3, 2, -1 }, new[] { -1, 0, 1, 2, 3 })]
        [TestCase("G", new[] { 1, -5, 3, 2, -1, -2, 10 }, new[] { -5, -2, -1, 1, 2, 3, 10 })]
        public void Test_1(string name, int[] input, int[] expected)
        {
            var f0 = new Launcher();
            f0.MergeSort(input, 0,input.Length-1);
            input.ShouldBe(expected);
        }
    }
}
