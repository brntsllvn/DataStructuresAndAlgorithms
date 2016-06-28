using System;
using System.Diagnostics;
using NUnit.Framework;
using Shouldly;

namespace MajorityElement
{
    [TestFixture]
    class MajorityElementTests
    {
        [TestCase("A", new long[] { 2 }, 1)]
        [TestCase("B", new long[] { 2, 1 }, 0)]
        [TestCase("C", new long[] { 2, 2 }, 1)]
        [TestCase("D0", new long[] { 2, 2, 2 }, 1)]
        [TestCase("D1", new long[] { 2, 2, 3 }, 1)]
        [TestCase("D2", new long[] { 2, 3, 4 }, 0)]
        //[TestCase("E0", new long[] { 1, 2, 3, 4 }, 0)]
        //[TestCase("E1", new long[] { 1, 1, 3, 4 }, 0)]
        //[TestCase("E2", new long[] { 0, 1, 1, 4 }, 0)]
        //[TestCase("E3", new long[] { 0, 1, 2, 2 }, 0)]
        //[TestCase("E4", new long[] { 0, 2, 2, 2 }, 1)]
        //[TestCase("E5", new long[] { 2, 2, 2, 0 }, 1)]
        //[TestCase("E6", new long[] { 2, 2, 2, 2 }, 1)]
        //[TestCase("F", new long[] { 2, 2, 2, 3, 9, }, 1)]
        //[TestCase("G", new long[] { 1, 2, 3, 4, 5, 6 }, 0)]
        //[TestCase("H", new long[] { 1, 2, 2, 2, 3 }, 1)]
        //[TestCase("I", new long[] { 1, 1, 2, 2, 2 }, 1)]
        //[TestCase("J", new long[] { 1, 1, 2, 2, 2, 2, 2, 2, 3, 3 }, 1)]
        //[TestCase("K", new long[] { 1, 1, 1, 1, 2, 2, 3, 3, 3, 3, 3 }, 0)]
        //[TestCase("L", new long[] { 1, 1, 1, 1, 2, 2, 3, 3, 3, 3 }, 0)]
        ////[TestCase("M", new long[] { 1, 1, 1, 2, 2 }, 1)]
        public void Test_1(string testCase, long[] input, int expected)
        {
            var f0 = new Launcher();
            f0.Majority(input, 0, input.Length - 1).ShouldBe(expected);
        }
    }
}
