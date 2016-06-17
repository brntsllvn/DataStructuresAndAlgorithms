using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;

namespace FibonacciMod
{
    //[TestFixture]
    //class FibonacciModTests
    //{
    //    [TestCase(new long[] { 1, 2 }, 1)]
    //    [TestCase(new long[] { 1, 3 }, 1)]
    //    [TestCase(new long[] { 1, 8 }, 1)]
    //    [TestCase(new long[] { 2, 2 }, 1)]
    //    [TestCase(new long[] { 2, 3 }, 1)]
    //    [TestCase(new long[] { 2, 8 }, 1)]
    //    [TestCase(new long[] { 3, 2 }, 0)]
    //    [TestCase(new long[] { 3, 3 }, 2)]
    //    [TestCase(new long[] { 3, 4 }, 2)]
    //    [TestCase(new long[] { 4, 2 }, 1)]
    //    [TestCase(new long[] { 4, 3 }, 0)]
    //    [TestCase(new long[] { 4, 4 }, 3)]
    //    [TestCase(new long[] { 4, 5 }, 3)]
    //    //[TestCase(new long[] { 281621358815590, 30524 }, 11963)]
    //    public void Test_1(long[] inputLongs, long expected)
    //    {
    //        var f0 = new Launcher();
    //        f0.CalculateFibonacciMod(inputLongs).ShouldBe(expected);

    //        Should.CompleteIn(
    //            () => f0.CalculateFibonacciMod(inputLongs), TimeSpan.FromMilliseconds(1500));

    //        var proc = Process.GetCurrentProcess();
    //        proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
    //    }
    //}
}
