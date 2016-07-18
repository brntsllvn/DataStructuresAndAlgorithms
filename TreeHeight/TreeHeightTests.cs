using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace TreeHeight
{
    [TestFixture]
    class TreeHeightTests
    {
        [TestCase("A", new long[] { 0 }, "hello")]
        public void Test_1(string caseName, long[] input, string expected)
        {
            var f0 = new Launcher();
            f0.CalculateTreeHeight(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.CalculateTreeHeight(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
