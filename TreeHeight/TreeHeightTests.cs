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
        [TestCase("C", 3, new[] { -1, 0, 1 }, 3)]
        [TestCase("D", 4, new[] { -1, 0, 1, 2 }, 4)]
        [TestCase("E", 5, new[] { -1, 0, 1, 2, 3 }, 5)]
        [TestCase("E", 6, new[] { -1, 0, 1, 2, 3, 4 }, 6)]
        [TestCase("F", 5, new[] { 4, -1, 4, 1, 1 }, 3)]
        [TestCase("G", 5, new[] { -1, 0, 4, 0, 3 }, 4)]
        public void Test_1(string caseName, int numberVertices, int[] input, int expected)
        {
            var f0 = new Launcher();
            var tree = f0.ConstructTreeFromArray(input);
            var treeHeight = f0.CalculateTreeHeight(tree);
            treeHeight.ShouldBe(expected);

            Should.CompleteIn(
                () => f0.CalculateTreeHeight(tree), TimeSpan.FromMilliseconds(1500));

            var proc = Process.GetCurrentProcess();
            proc.PrivateMemorySize64.ShouldBeLessThanOrEqualTo(512 * 1000 * 1000);
        }
    }
}
