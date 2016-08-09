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

            if (expectedArray.Length == 0)
            {
                program.Result.ShouldBeEmpty();
                return;
            }

            for (int i = 0; i < inputTreeNodes.Count; i++)
                program.Result[i].ShouldBe(expectedArray[i]);
        }

        private static readonly object[] InputOutput =
        {
            new object[]
            { "A",
                new List<TreeNode> {
                    new TreeNode()
                },
                new long[] {}
            },
            new object[]
            { "B",
                new List<TreeNode> {
                    new TreeNode(10,-1,-1)
                },
                new long[] {10}
            },
            new object[]
            { "C",
                new List<TreeNode> {
                    new TreeNode(10,1,-1),
                    new TreeNode(11,-1,-1),
                },
                new long[] {11,10}
            },
            new object[]
            { "D",
                new List<TreeNode> {
                    new TreeNode(10,1,2),
                    new TreeNode(11,-1,-1),
                    new TreeNode(12,-1,-1),
                },
                new long[] {11,10,12}
            },
            new object[]
            { "E",
                new List<TreeNode> {
                    new TreeNode(10,1,2),
                    new TreeNode(11,3,-1),
                    new TreeNode(12,-1,-1),
                    new TreeNode(13,-1,-1),
                },
                new long[] {13,11,10,12}
            },
            new object[]
            { "F",
                new List<TreeNode> {
                    new TreeNode(10,1,2),
                    new TreeNode(11,3,4),
                    new TreeNode(12,-1,-1),
                    new TreeNode(13,-1,-1),
                    new TreeNode(14,-1,-1),
                },
                new long[] {13,11,14,10,12}
            },
        };
    }
}
