﻿using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace QuickSort
{
    [TestFixture]
    class QuickSortTests
    {
        [TestCase(new long[] { 0 }, "hello")]
        public void Test_1(long[] input, string expected)
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
