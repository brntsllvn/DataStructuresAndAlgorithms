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
        public void Test_1(string name, int[] input, int[] expected)
        {
            var f0 = new Launcher();
            f0.MergeSort(input, 0,input.Length-1);
            input.ShouldBe(expected);
        }
    }
}
