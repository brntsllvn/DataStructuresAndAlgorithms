using NUnit.Framework;
using Shouldly;

namespace BubbleSortDemo

{
    [TestFixture]
    class BubbleSortDemoTests
    {
        [TestCase("A", new int[] { 0 }, new int[] { 0 })]
        [TestCase("B", new int[] { 0, 1 }, new int[] { 0, 1 })]
        [TestCase("C", new int[] { 0, 1, -1 }, new int[] { -1, 0, 1 })]
        [TestCase("D", new int[] { 5, 1, -1, 0 }, new int[] { -1, 0, 1, 5 })]
        public void Test_1(string caseName, int[] input, int[] expected)
        {
            var f0 = new Launcher();
            f0.BubbleSort(input);
            input.ShouldBe(expected);
        }
    }
}
