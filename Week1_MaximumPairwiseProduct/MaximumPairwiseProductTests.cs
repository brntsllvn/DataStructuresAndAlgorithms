using System;
using NUnit.Framework;
using Shouldly;

namespace Week1_MaximumPairwiseProduct
{
    [TestFixture]
    class MaximumPairwiseProductTests
    {
        [TestCase(new long[] { 1, 2, 3 }, 6, 1)]
        [TestCase(new long[] { 7, 5, 14, 2, 8, 8, 10, 1, 2, 3 }, 140, 1)]
        [TestCase(new long[] { 4, 6, 2, 6, 1 }, 36, 1)]
        [TestCase(new long[] { 100000, 90000 }, 9000000000, 1)]
        public void Pairwise_Product1(long[] longArr, long expectedProduct, int completionTime)
        {
            var product = new Launcher().FindMaxPairwiseProduct(longArr);

            product.ShouldBe(expectedProduct);
            Should.CompleteIn(() => product, TimeSpan.FromMilliseconds(completionTime));
            long memory = GC.GetTotalMemory(true);
            memory.ShouldBeLessThanOrEqualTo(1000000);
        }
    }
}
