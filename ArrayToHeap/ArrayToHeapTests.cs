using ArrayToHeap;
using NUnit.Framework;
using Shouldly;

namespace LauncherTemplate
{
    [TestFixture]
    class ArrayToHeapTests
    {
        [TestCase("A", new long[] { 0 }, new long[] { 0 })]
        public void Array_To_Heap(string caseName, long[] input, long[] expected)
        {
            var f0 = new Launcher();
            f0.GenerateSwaps(input);
            input.ShouldBe(expected);
        }

        [Test]
        public void Swap_Works()
        {
            var swapper = new Swap(1, 2);
            swapper.i.ShouldBe(1);
            swapper.j.ShouldBe(2);
        }
    }
}
