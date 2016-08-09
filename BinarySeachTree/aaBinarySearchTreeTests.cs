using BinarySeachTree;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace BinarySearchTree
{
    [TestFixture]
    class aaBinarySearchTreeTests
    {
        [Test, TestCaseSource(nameof(Convert))]
        public void ConvertArrayToSingleLineString(string caseName, List<long?> input, string expected)
        {
            var program = new Program();
            var output = program.ToSpecialString(input);
            output.ShouldBe(expected);
        }

        private static readonly object[] Convert =
        {
            new object[]
            { "A",new List<long?> {1,2,3},"1 2 3"}
        };

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

        [Test, TestCaseSource(nameof(PostOrderData))]
        public void PostOrderTraversal_Tests(string caseName, List<TreeNode> inputTreeNodes, long[] expectedArray)
        {
            var program = new Program();
            foreach (var treeNode in inputTreeNodes)
                program.TreeNodes.Add(treeNode);

            var root = program.TreeNodes[0];
            program.PostOrderTraversal(root);

            if (expectedArray.Length == 0)
            {
                program.PostOrderTraversalResult.ShouldBeEmpty();
                return;
            }

            for (int i = 0; i < inputTreeNodes.Count; i++)
                program.PostOrderTraversalResult[i].ShouldBe(expectedArray[i]);
        }

        private static readonly object[] PostOrderData =
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
                new long[] {11,12,10}
            },
            new object[]
            { "E",
                new List<TreeNode> {
                    new TreeNode(10,1,2),
                    new TreeNode(11,3,-1),
                    new TreeNode(12,-1,-1),
                    new TreeNode(13,-1,-1),
                },
                new long[] {13,11,12,10}
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
                new long[] {13,14,11,12,10}
            },
            new object[]
            { "G",
                new List<TreeNode> {
                    new TreeNode(4,1,2),
                    new TreeNode(2,3,4),
                    new TreeNode(5,-1,-1),
                    new TreeNode(1,-1,-1),
                    new TreeNode(3,-1,-1),
                },
                new long[] {1,3,2,5,4}
            },
            new object[]
            { "H",
                new List<TreeNode> {
                    new TreeNode(0,7,2),
                    new TreeNode(10,-1,-1),
                    new TreeNode(20,-1,6),
                    new TreeNode(30,8,9),
                    new TreeNode(40,3,-1),
                    new TreeNode(50,-1,-1),
                    new TreeNode(60,1,-1),
                    new TreeNode(70,5,4),
                    new TreeNode(80,-1,-1),
                    new TreeNode(90,-1,-1),
                },
                new long[] {50,80,90,30,40,70,10,60,20,0}
            },
        };

        [Test, TestCaseSource(nameof(PreOrderData))]
        public void PreOrderTraversal_Tests(string caseName, List<TreeNode> inputTreeNodes, long[] expectedArray)
        {
            var program = new Program();
            foreach (var treeNode in inputTreeNodes)
                program.TreeNodes.Add(treeNode);

            var root = program.TreeNodes[0];
            program.PreOrderTraversal(root);

            if (expectedArray.Length == 0)
            {
                program.PreOrderTraversalResult.ShouldBeEmpty();
                return;
            }

            for (int i = 0; i < inputTreeNodes.Count; i++)
                program.PreOrderTraversalResult[i].ShouldBe(expectedArray[i]);
        }

        private static readonly object[] PreOrderData =
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
                new long[] {10,11}
            },
            new object[]
            { "D",
                new List<TreeNode> {
                    new TreeNode(10,1,2),
                    new TreeNode(11,-1,-1),
                    new TreeNode(12,-1,-1),
                },
                new long[] {10,11,12}
            },
            new object[]
            { "E",
                new List<TreeNode> {
                    new TreeNode(10,1,2),
                    new TreeNode(11,3,-1),
                    new TreeNode(12,-1,-1),
                    new TreeNode(13,-1,-1),
                },
                new long[] {10,11,13,12}
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
                new long[] { 10, 11, 13, 14, 12 }
            },
            new object[]
            { "G",
                new List<TreeNode> {
                    new TreeNode(4,1,2),
                    new TreeNode(2,3,4),
                    new TreeNode(5,-1,-1),
                    new TreeNode(1,-1,-1),
                    new TreeNode(3,-1,-1),
                },
                new long[] {4,2,1,3,5}
            },
            new object[]
            { "H",
                new List<TreeNode> {
                    new TreeNode(0,7,2),
                    new TreeNode(10,-1,-1),
                    new TreeNode(20,-1,6),
                    new TreeNode(30,8,9),
                    new TreeNode(40,3,-1),
                    new TreeNode(50,-1,-1),
                    new TreeNode(60,1,-1),
                    new TreeNode(70,5,4),
                    new TreeNode(80,-1,-1),
                    new TreeNode(90,-1,-1),
                },
                new long[] {0,70,50,40,30,80,90,20,60,10}
            },
        };

        [Test, TestCaseSource(nameof(InOrderData))]
        public void InOrderTraversal_Tests(string caseName, List<TreeNode> inputTreeNodes, long[] expectedArray)
        {
            var program = new Program();
            foreach (var treeNode in inputTreeNodes)
                program.TreeNodes.Add(treeNode);

            var root = program.TreeNodes[0];
            program.InOrderTraversal(root);

            if (expectedArray.Length == 0)
            {
                program.InOrderTraversalResult.ShouldBeEmpty();
                return;
            }

            for (int i = 0; i < inputTreeNodes.Count; i++)
                program.InOrderTraversalResult[i].ShouldBe(expectedArray[i]);
        }

        private static readonly object[] InOrderData =
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
            new object[]
            { "G",
                new List<TreeNode> {
                    new TreeNode(4,1,2),
                    new TreeNode(2,3,4),
                    new TreeNode(5,-1,-1),
                    new TreeNode(1,-1,-1),
                    new TreeNode(3,-1,-1),
                },
                new long[] {1,2,3,4,5}
            },
            new object[]
            { "H",
                new List<TreeNode> {
                    new TreeNode(0,7,2),
                    new TreeNode(10,-1,-1),
                    new TreeNode(20,-1,6),
                    new TreeNode(30,8,9),
                    new TreeNode(40,3,-1),
                    new TreeNode(50,-1,-1),
                    new TreeNode(60,1,-1),
                    new TreeNode(70,5,4),
                    new TreeNode(80,-1,-1),
                    new TreeNode(90,-1,-1),
                },
                new long[] {50,70,80,30,90,40,0,20,10,60}
            },
        };
    }
}
