using NUnit.Framework;
using Shouldly;

namespace PrimitiveCalculator
{
    [TestFixture]
    class PrimitiveCalculatorTests
    {
        [TestCase(2, 2, new[] { 1, 2 })]
        [TestCase(3, 2, new[] { 1, 3 })]
        [TestCase(4, 3, new[] { 1, 3, 4 })]
        [TestCase(5, 4, new[] { 1, 3, 4, 5 })]
        [TestCase(6, 3, new[] { 1, 3, 6 })]
        [TestCase(7, 4, new[] { 1, 3, 6, 7 })]
        [TestCase(8, 4, new[] { 1, 3, 4, 8 })]
        [TestCase(9, 3, new[] { 1, 3, 9 })]
        [TestCase(10, 4, new[] { 1, 3, 9, 10 })]
        [TestCase(11, 5, new[] { 1, 3, 9, 10, 11 })]
        [TestCase(12, 4, new[] { 1, 3, 6, 12 })]
        [TestCase(13, 5, new[] { 1, 3, 6, 12, 13 })]
        [TestCase(14, 5, new[] { 1, 3, 6, 7, 14 })]
        [TestCase(15, 5, new[] { 1, 3, 4, 5, 15 })]
        [TestCase(16, 5, new[] { 1, 3, 4, 8, 16 })]
        [TestCase(17, 6, new[] { 1, 3, 4, 8, 16, 17 })]
        [TestCase(18, 4, new[] { 1, 3, 9, 18 })]
        [TestCase(19, 5, new[] { 1, 3, 9, 18, 19 })]
        [TestCase(20, 5, new[] { 1, 3, 9, 10, 20 })]
        [TestCase(21, 5, new[] { 1, 3, 6, 7, 21 })]
        [TestCase(11809, 13, new[] { 1, 3, 9, 27, 81, 82, 246, 738, 1476, 2952, 5904, 11808, 11809 })]

        public void NumOpsTests(int n, int expectedLength, int[] expectedNumOpertations)
        {
            var f0 = new Launcher();
            f0.PrimitiveCalculatorOperationCounter(n).Length.ShouldBe(expectedLength);
            f0.PrimitiveCalculatorOperationCounter(n).ShouldBe(expectedNumOpertations);
        }
    }
}
