using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace BinarySearch
{
    [TestFixture]
    class BinarySearchTests
    {
        [TestCase(new long[] { 1, 13 }, new long[] { 1, 13 }, "0")]
        [TestCase(new long[] { 1, 13 }, new long[] { 2, 13, 13 }, "0 0")]
        [TestCase(new long[] { 1, 13 }, new long[] { 3, 13, 13, 13 }, "0 0 0")]
        [TestCase(new long[] { 1, 13 }, new long[] { 1, 7 }, "-1")]
        [TestCase(new long[] { 1, 13 }, new long[] { 2, 7, 7 }, "-1 -1")]
        [TestCase(new long[] { 1, 13 }, new long[] { 3, 7, 7, 7 }, "-1 -1 -1")]
        [TestCase(new long[] { 1, 13 }, new long[] { 3, 7, 13, 7 }, "-1 0 -1")]
        [TestCase(new long[] { 2, 13, 87 }, new long[] { 1, 13 }, "0")]
        [TestCase(new long[] { 2, 13, 87 }, new long[] { 1, 87 }, "1")]
        //[TestCase(new long[] { 3, 13, 87, 13 }, new long[] { 2, 87, 13 }, "1 0")]
        //[TestCase(new long[] { 5, 1, 5, 8, 12, 13 }, new long[] { 5, 8, 1, 23, 1, 11 }, "2 0 -1 0 -1")]
        public void BinarySearch_GameOn(long[] data, long[] searchTerms, string expected)
        {
            var f0 = new Launcher();
            f0.BinarySearchSetup(data, searchTerms).ShouldBe(expected);

            Should.CompleteIn(
            () => f0.BinarySearchSetup(data, searchTerms), TimeSpan.FromMilliseconds(3000));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
