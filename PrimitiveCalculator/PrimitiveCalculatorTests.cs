using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PrimitiveCalculator
{
    [TestFixture]
    class PrimitiveCalculatorTests
    {
        [TestCase(1, 0)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(4, 2)]
        [TestCase(5, 3)]
        [TestCase(8, 3)]
        [TestCase(12, 3)]
        [TestCase(16, 4)]
        [TestCase(96234, 14)]
        public void Test_1(int n, int expectedNumOpertations)
        {
            var f0 = new Launcher();
            f0.PrimitiveCalculatorOperationCounter(n).ShouldBe(expectedNumOpertations);
        }
    }
}
