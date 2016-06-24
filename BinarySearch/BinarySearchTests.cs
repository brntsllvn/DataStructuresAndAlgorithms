using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace BinarySearch
{
    [TestFixture]
    class BinarySearchTests
    {
        [TestCase(new long[] { 13 }, new long[] { 13 }, "0")] // why is this case so slow?
        [TestCase(new long[] { 13 }, new long[] { 13, 13 }, "0 0")]
        [TestCase(new long[] { 13 }, new long[] { 13, 13, 13 }, "0 0 0")]
        [TestCase(new long[] { 13 }, new long[] { 7 }, "-1")]
        [TestCase(new long[] { 13 }, new long[] { 7, 7 }, "-1 -1")]
        [TestCase(new long[] { 13 }, new long[] { 7, 7, 7 }, "-1 -1 -1")]
        [TestCase(new long[] { 13 }, new long[] { 7, 13, 7 }, "-1 0 -1")]
        [TestCase(new long[] { 13, 87 }, new long[] { 13 }, "0")]
        [TestCase(new long[] { 13, 87 }, new long[] { 87 }, "1")]
        [TestCase(new long[] { 13, 87, 13 }, new long[] { 87, 13 }, "1 0")]
        [TestCase(new long[] { 1, 5, 8, 12, 13 }, new long[] { 87, 13 }, "-1 4")]
        [TestCase(new long[] { 1, 5, 8, 12, 13 }, new long[] { 8, 1, 23, 1, 11 }, "2 0 -1 0 -1")]
        [TestCase(new long[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
                  new long[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 },
                  "-1 0 1 2 3 4 5 6 7 8 9 -1")]
        public void BinarySearch_GameOn(long[] data, long[] searchTerms, string expected)
        {
            var f0 = new Launcher();
            f0.BinarySearchSetup(data, searchTerms).ShouldBe(expected);

            Should.CompleteIn(
            () => f0.BinarySearchSetup(data, searchTerms), TimeSpan.FromMilliseconds(10));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
