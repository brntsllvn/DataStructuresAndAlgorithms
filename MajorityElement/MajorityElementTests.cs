using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace MajorityElement
{
    class MajorityElementTests
    {
        [TestCase(new long[] { 2 }, 1)]
        [TestCase(new long[] { 2, 1 }, 0)]
        [TestCase(new long[] { 2, 2 }, 1)]
        [TestCase(new long[] { 2, 2, 2 }, 1)]
        //[TestCase(new long[] { 2, 2, 2, 3, 9, }, 1)]
        //[TestCase(new long[] { 1, 2, 3, 4, 5, 6 }, 0)]
        //[TestCase(new long[] { 1, 1, 2, 3 }, 0)]
        //[TestCase(new long[] { 1, 2, 2, 2, 3 }, 1)]
        //[TestCase(new long[] { 1, 1, 2, 2, 2 }, 1)]
        public void Test_1(long[] input, int expected)
        {
            var f0 = new Launcher();
            f0.HasMajorityElement(input, input.Length).ShouldBe(expected);
        }
    }
}
