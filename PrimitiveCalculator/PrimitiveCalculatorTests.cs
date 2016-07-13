using NUnit.Framework;
using Shouldly;

namespace PrimitiveCalculator
{
    [TestFixture]
    class PrimitiveCalculatorTests
    {
        [TestCase(1, 0, new int[0])]
        //[TestCase(2, 2, new int[2] { 1, 2 })]
        [TestCase(3, 2, new int[2] { 1, 3 } )]
        //[TestCase(4, 2)]
        //[TestCase(5, 3)]
        //[TestCase(6, 2)]
        //[TestCase(7, 3)]
        //[TestCase(8, 3)]
        //[TestCase(9, 2)]
        //[TestCase(10, 3)]
        //[TestCase(11, 4)]
        //[TestCase(12, 3)]
        //[TestCase(16, 4)]
        //[TestCase(96234, 14)]
        public void NumOpsTests(int n, int expectedLength, int[] expectedNumOpertations)
        {
            var f0 = new Launcher();
            //f0.PrimitiveCalculatorOperationCounter(n).Length.ShouldBe(expectedLength);
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
