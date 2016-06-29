using NUnit.Framework;
using Shouldly;

namespace MergeSort
{
    [TestFixture]
    class MergeSortTests
    {
        [TestCase("A", new long[] { 0 }, new long[] { 0 })]
        public void Test_1(string name, long[] input, long[] expected)
        {
            var f0 = new Launcher();
            f0.MergeSort(input).ShouldBe(expected);

            //Should.CompleteIn(
            //    () => f0.MergeSort(input), TimeSpan.FromMilliseconds(1500));

            //var proc = Process.GetCurrentProcess();
            //proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
