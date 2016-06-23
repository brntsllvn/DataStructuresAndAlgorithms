using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace MinDotProd
{
    [TestFixture]
    class MinDotProdTests
    {
        [TestCase(new long[1] { 23 }, new long[1] { 39 }, 897)]
        [TestCase(new long[1] { 99999 }, new long[1] { 99999 }, 9999800001)]
        [TestCase(new long[2] { 5, 5 }, new long[2] { 0, 3 }, 15)]
        [TestCase(new long[3] { 1, 3, -5 }, new long[3] { -2, 4, 1 }, -25)]
        [TestCase(new long[2] { -10000, 10000 }, new long[2] { -1, 1 }, -20000)]
        public void Test_1(long[] first, long[] second, long expected)
        {
            var f0 = new Launcher();
            f0.ComputeMinDotProduct(first, second).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.ComputeMinDotProduct(first, second), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
