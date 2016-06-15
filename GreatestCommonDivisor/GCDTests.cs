using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;
namespace GreatestCommonDivisor
{
    [TestFixture]
    class GcdTests
    {
        [TestCase(new long[] {1, 1}, 1)]
        [TestCase(new long[] {3, 5}, 1)]
        [TestCase(new long[] {1024, 32}, 32)]
        [TestCase(new long[] {28851538, 1183019}, 17657)]
        [TestCase(new long[] { 226553150, 1023473145 }, 5)]
        [TestCase(new long[] { 2, 2 * 1000 * 1000 * 1000 }, 2)]
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
