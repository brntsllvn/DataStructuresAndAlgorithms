using BinarySeachTree;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

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

        [Test, TestCaseSource(nameof(InputOutput))]
        public void InOrderTraversal_Tests(string caseName, List<TreeNode> inputTreeNodes, long[] expectedArray)
        {
            var program = new Program();
            foreach (var treeNode in inputTreeNodes)
                program.TreeNodes.Add(treeNode);

            var root = program.TreeNodes[0];
            program.InOrderTraversal(root);

            program.InOrderTraversalResult.ShouldBe(expectedArray);
        }

        private static readonly object[] InputOutput =
        {
            new object[] 
            { "A",
                new List<TreeNode> {
                    new TreeNode(10,-1,-1)
                },
                new long[] {10}
            }
        };
    }
}
