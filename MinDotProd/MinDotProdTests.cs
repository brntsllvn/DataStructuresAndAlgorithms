using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace MinDotProd
{
    [TestFixture]
    class MinDotProdTests
    {
        [TestCase(new int[1] { 23 }, new int[1] { 39 }, 897)]
        [TestCase(new int[3] { 1, 3, -5 }, new int[3] { -2, 4, 1 }, -25)]
        public void Test_1(int[] first, int[] second, int expected)
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
