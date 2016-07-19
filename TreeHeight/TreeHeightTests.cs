using NUnit.Framework;
using Shouldly;
using System;
using System.Diagnostics;

namespace TreeHeight
{
    [TestFixture]
    class TreeHeightTests
    {
        [TestCase("A", 1, new[] { 0 }, 1)]
        [TestCase("B", 2, new[] { -1, 0 }, 2)]
        public void Test_1(string caseName, int numberVertices, int[] input, int expected)
        {
            //var f0 = new Launcher();
            //var treeHeight = f0.CalculateTreeHeight(input);
            //treeHeight.ShouldBe(expected);

            //Should.CompleteIn(
            //    () => f0.CalculateTreeHeight(input), TimeSpan.FromMilliseconds(1500));

            //var proc = Process.GetCurrentProcess();
            //proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
