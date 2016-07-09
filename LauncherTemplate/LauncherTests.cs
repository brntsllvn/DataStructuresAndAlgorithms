using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace LauncherTemplate
{
    [TestFixture]
    class FibonacciTests
    {
        [TestCase("A", new long[] {0}, "hello")]
        public void Test_1(string caseName, long[] input, string expected)
        {
            var f0 = new Launcher();
            f0.MyFunction(input).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.MyFunction(input), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
