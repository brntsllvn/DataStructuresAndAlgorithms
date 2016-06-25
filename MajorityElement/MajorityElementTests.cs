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
        [TestCase("D", new long[] { 2, 2, 2 }, 1)]
        [TestCase("E", new long[] { 2, 2, 2, 3, 9, }, 1)]
        //[TestCase("F", new long[] { 1, 2, 3, 4, 5, 6 }, 0)]
        //[TestCase("G", new long[] { 1, 1, 2, 3 }, 0)]
        //[TestCase("H", new long[] { 1, 2, 2, 2, 3 }, 1)]
        //[TestCase("I", new long[] { 1, 1, 2, 2, 2 }, 1)]
        //[TestCase("J", new long[] { 1, 1, 2, 2, 2, 2, 2, 2, 3, 3 }, 0)]
        public void Test_1(string testCase, long[] input, int expected)
        {
            var f0 = new Launcher();
            f0.HasMajorityElement(input, input.Length).ShouldBe(expected);
        }
    }
}
