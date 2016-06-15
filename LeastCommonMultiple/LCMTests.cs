using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace LeastCommonMultiple
{
    [TestFixture]
    class LcmTests
    {
        [TestCase(new long[] { 1, 1 }, 1)]
        [TestCase(new long[] { 6, 8 }, 24)]
        [TestCase(new long[] { 330, 65 }, 4290)]
        [TestCase(new long[] { 330, 330 }, 330)]
        [TestCase(new long[] { 1571, 97 }, 152387)]
        [TestCase(new long[] { 28851538, 1183019 }, 1933053046)]
        [TestCase(new long[] { 28851538, 28851538 }, 28851538)]
        [TestCase(new long[] { 226553150, 1023473145 }, 46374212988031350)]
        [TestCase(new long[] { 2, 2 * 1000 * 1000 * 1000 }, 2 * 1000 * 1000 * 1000)]
        public void Test_1(long[] inputLongs, long expected)
        {
            var f0 = new Launcher();
            f0.CalculateLcm(inputLongs).ShouldBe(expected);

            Should.CompleteIn(
                () => f0.CalculateLcm(inputLongs), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
