using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;
namespace GreatestCommonDivisor
{
    [TestFixture]
    class FibonacciTests
    {
        [TestCase(0, 1, 0)]
        [TestCase(3, 5, 1)]
        [TestCase(2, 2*1000*1000*1000, 2)]
        public void Test_1(long[] inputLongs, long expected)
        {
            var f0 = new Launcher();
            f0.CalculateGcd(inputLongs).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.CalculateGcd(inputLongs), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
