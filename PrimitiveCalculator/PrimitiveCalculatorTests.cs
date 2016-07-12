using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace PrimitiveCalculator
{
    [TestFixture]
    class PrimitiveCalculatorTests
    {
        [TestCase(1, 0, new int[] { 1 })]
        public void Test_1(int n, int expectedNumOpertations, int[] expectedIntermediateInts)
        {
            var f0 = new Launcher();
            f0.MyFunction(n).ShouldBe(expectedNumOpertations);

            Should.CompleteIn(() => f0.MyFunction(n), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
