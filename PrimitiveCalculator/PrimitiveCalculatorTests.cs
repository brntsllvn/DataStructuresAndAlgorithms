using NUnit.Framework;
using Shouldly;

namespace PrimitiveCalculator
{
    [TestFixture]
    class PrimitiveCalculatorTests
    {
        [TestCase(1,  0, new int[0])]
        [TestCase(2,  2, new[] { 1, 2 })]
        [TestCase(3,  2, new[] { 1, 3 })]
        [TestCase(4,  3, new[] { 1, 2, 4 })]
        [TestCase(5,  4, new[] { 1, 2, 4, 5 })]
        [TestCase(6,  3, new[] { 1, 2, 6 })]
        [TestCase(7,  4, new[] { 1, 2, 6, 7 })]
        [TestCase(8,  4, new[] { 1, 2, 4, 8 })]
        [TestCase(9,  3, new[] { 1, 3, 9 })]
        [TestCase(10, 4, new[] { 1, 3, 9, 10 })]
        [TestCase(11, 5, new[] { 1, 3, 9, 10, 11 })]
        [TestCase(12, 4, new[] { 1, 2, 4, 12 })]
        [TestCase(16, 5, new[] { 1, 2, 4, 8, 16 })]
        [TestCase(96234, 15, new[] { 1, 2, 6, 7, 21, 22, 66, 198, 594, 1782, 5346, 16038, 16039, 32078, 96234 })]
        public void NumOpsTests(int n, int expectedLength, int[] expectedNumOpertations)
        {
            var f0 = new Launcher();
            f0.PrimitiveCalculatorOperationCounter(n).Length.ShouldBe(expectedLength);
            f0.PrimitiveCalculatorOperationCounter(n).ShouldBe(expectedNumOpertations);
        }

        [TestCase(0, 2, false)]
        [TestCase(1, 2, true)]
        [TestCase(2, 2, true)]
        [TestCase(3, 2, false)]
        [TestCase(7, 2, false)]
        [TestCase(16, 2, true)]
        [TestCase(57, 2, false)]
        [TestCase(1024, 2, true)]
        [TestCase(1, 3, true)]
        [TestCase(2, 3, false)]
        [TestCase(3, 3, true)]
        [TestCase(4, 3, false)]
        [TestCase(27, 3, true)]
        [TestCase(28, 3, false)]
        [TestCase(59049, 3, true)]
        public void IsPow(int n, int x, bool expected)
        {
            var f0 = new Launcher();
            f0.IsPowerOfX(n, x).ShouldBe(expected);
        }
    }
}
