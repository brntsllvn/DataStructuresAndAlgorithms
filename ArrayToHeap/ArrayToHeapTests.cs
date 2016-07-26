using ArrayToHeap;
using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace LauncherTemplate
{
    [TestFixture]
    class ArrayToHeapTests
    {
        [Test]
        public void Swap_Works()
        {
            var swapper = new Swap(1, 2);
            swapper.i.ShouldBe(1);
            swapper.j.ShouldBe(2);
        }

        [TestCase("A", new long[] { 0 }, "hello")]
        public void Array_To_Heap_Test(string caseName, long[] input, string expected)
        {
            var f0 = new Launcher();
            f0.MyFunction(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.MyFunction(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
