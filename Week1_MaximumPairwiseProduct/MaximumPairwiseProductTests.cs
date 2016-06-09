using NUnit.Framework;
using Shouldly;

namespace Week1_MaximumPairwiseProduct
{
    [TestFixture]
    class MaximumPairwiseProductTests
    {
        [Test]
        public void Pairwise_Product1()
        {
            var arr = new int[] { 1, 2, 3 };
            var product = new Launcher().FindMaxPairwiseProduct(arr);

            product.ShouldBe(6);
        }

        [Test]
        public void Pairwise_Product2()
        {
            var arr = new int[] { 7, 5, 14, 2, 8, 8, 10, 1, 2, 3 };
            var product = new Launcher().FindMaxPairwiseProduct(arr);

            product.ShouldBe(140);
        }

        [Test]
        public void Pairwise_Product3()
        {
            var arr = new int[] { 4, 6, 2, 6, 1 };
            var product = new Launcher().FindMaxPairwiseProduct(arr);

            product.ShouldBe(36);
        }
    }
}
