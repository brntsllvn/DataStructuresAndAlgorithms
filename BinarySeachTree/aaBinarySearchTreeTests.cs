using BinarySeachTree;
using NUnit.Framework;
using Shouldly;

namespace BinarySearchTree
{
    [TestFixture]
    class aaBinarySearchTreeTests
    {
        [TestCase("A", new long[] { 1, 2, 3 })]
        public void ConvertRawDataToTreeNode(string caseName, long[] inputArr)
        {
            var program = new Program();
            program.AddTreeNode(inputArr);

            var newTreeNode = program.TreeNodes[0];

            newTreeNode.Key.ShouldBe(1);
            newTreeNode.LeftChildIndex.ShouldBe(2);
            newTreeNode.RightChildIndex.ShouldBe(3);
        }

        [TestCase("A", new long[] { 0 }, "hello")]
        public void Test_1(string caseName, long[] input, string expected)
        {
            var f0 = new Program();
            var result = f0.MagicFunctionThatSolvesAllProblems();
            result.ShouldBe(expected);
        }
    }
}
