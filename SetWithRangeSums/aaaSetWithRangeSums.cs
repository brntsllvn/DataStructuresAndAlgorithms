using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace SetWithRangeSums
{
    [TestFixture]
    class aaaSetWithRangeSums
    {
        [Test, TestCaseSource(nameof(Splay))]
        public void SplayNode_Test(string caseName, List<TreeNode> inputNodes,
            int indexOfNodeToSplay, List<TreeNode> expectedNodes)
        {
            var program = new Program();

            foreach (var node in inputNodes)
                program.TreeNodes.Add(node);

            var nodeToSplay = inputNodes[indexOfNodeToSplay];
            program.Splay(nodeToSplay);

            for (int i = 0; i < expectedNodes.Count; i++)
            {
                inputNodes[i].Value.ShouldBe(expectedNodes[i].Value);
                inputNodes[i].LeftChildIndex.ShouldBe(expectedNodes[i].LeftChildIndex);
                inputNodes[i].RightChildIndex.ShouldBe(expectedNodes[i].RightChildIndex);
                inputNodes[i].ParentIndex.ShouldBe(expectedNodes[i].ParentIndex);
            }
        }

        #region
        public static object[] Splay =
        {
            new object[] { "base",
                    new List<TreeNode> {
                        new TreeNode(1,-1,-1,-1)
                },
                0,
                    new List<TreeNode> {
                        new TreeNode(1,-1,-1,-1),
                }
            },
            new object[] { "zig A",
                    new List<TreeNode> {
                        new TreeNode(1,1,-1,-1),
                        new TreeNode(2,-1,-1,1)
                },
                0,
                    new List<TreeNode> {
                        new TreeNode(2,1,-1,-1),
                        new TreeNode(1,-1,-1,1)
                }
            },
        };
        #endregion

        [Test, TestCaseSource(nameof(ZigData))]
        public void Zig_Test(string caseName, List<TreeNode> inputNodes,
    int indexOfNodeToSplay, List<TreeNode> expectedNodes)
        {
            var program = new Program();

            foreach (var node in inputNodes)
                program.TreeNodes.Add(node);

            var nodeToSplay = inputNodes[indexOfNodeToSplay];
            program.Zig(nodeToSplay);

            for (int i = 0; i < expectedNodes.Count; i++)
            {
                inputNodes[i].Value.ShouldBe(expectedNodes[i].Value);
                inputNodes[i].LeftChildIndex.ShouldBe(expectedNodes[i].LeftChildIndex);
                inputNodes[i].RightChildIndex.ShouldBe(expectedNodes[i].RightChildIndex);
                inputNodes[i].ParentIndex.ShouldBe(expectedNodes[i].ParentIndex);
            }
        }

        #region
        public static object[] ZigData =
        {
            new object[] { "zig A",
                    new List<TreeNode> {
                        new TreeNode(0,1,-1,-1),
                        new TreeNode(1,-1,-1,0)
                },
                1,
                    new List<TreeNode> {
                        new TreeNode(1,1,-1,-1),
                        new TreeNode(0,-1,-1,0)
                }
            },
        };
        #endregion

        [Test, TestCaseSource(nameof(DetermineZigZigZag))]
        public void FDetermineZigZigZag_Test(string caseName, List<TreeNode> treeNodes,
            int indexOfNodeToSplay, string expectedCase)
        {
            var program = new Program();
            program.TreeNodes.AddRange(treeNodes);
            var splayNode = treeNodes[indexOfNodeToSplay];

            var zigcase = program.DetermineZigZigZag(splayNode);

            zigcase.ShouldBe(expectedCase);
        }

        #region
        public static object[] DetermineZigZigZag =
        {
            new object[]
            { "A",
                new List<TreeNode>
                {
                    new TreeNode(0,-1,-1,-1)
                },
                0,
                "none"
            },
            new object[]
            { "zig left",
                new List<TreeNode>
                {
                    new TreeNode(0,1,-1,-1),
                    new TreeNode(1,-1,-1,0),
                },
                1,
                "zig left"
            },
            new object[]
            { "zig right",
                new List<TreeNode>
                {
                    new TreeNode(0,-1,1,-1),
                    new TreeNode(1,-1,-1,0),
                },
                1,
                "zig right"
            },
            new object[]
            { "zigzig left",
                new List<TreeNode>
                {
                    new TreeNode(0,1,-1,-1),
                    new TreeNode(1,2,-1,0),
                    new TreeNode(2,-1,-1,1),
                },
                2,
                "zigzig left"
            },
            new object[]
            { "zigzig right",
                new List<TreeNode>
                {
                    new TreeNode(0,-1,1,-1),
                    new TreeNode(1,-1,2,0),
                    new TreeNode(2,-1,-1,1),
                },
                2,
                "zigzig right"
            },
            new object[]
            { "zigzag right",
                new List<TreeNode>
                {
                    new TreeNode(0,1,-1,-1),
                    new TreeNode(1,-1,2,0),
                    new TreeNode(2,-1,-1,1),
                },
                2,
                "zigzag right"
            },
            new object[]
            { "zigzag left",
                new List<TreeNode>
                {
                    new TreeNode(0,-1,1,-1),
                    new TreeNode(1,2,-1,0),
                    new TreeNode(2,-1,-1,1),
                },
                2,
                "zigzag left"
            },
        };
        #endregion

        [Test, TestCaseSource(nameof(FindGrandparentData))]
        public void FindGrandparent_Test(string caseName, List<TreeNode> treeNodes,
            int indexOfNodeToSplay, TreeNode expectedGrandparent)
        {
            var program = new Program();
            program.TreeNodes.AddRange(treeNodes);

            var splayNode = treeNodes[indexOfNodeToSplay];

            var grandparent = program.GetGrandparentNode(splayNode);

            var grandparentValue = grandparent.Value;
            grandparentValue.ShouldBe(expectedGrandparent.Value);
        }

        #region
        public static object[] FindGrandparentData =
        {
            new object[]
            { "A",
                new List<TreeNode>
                {
                    new TreeNode(0,-1,-1,-1)
                },
                0,
                new TreeNode()
            },
            new object[]
            { "B",
                new List<TreeNode>
                {
                    new TreeNode(0,1,-1,-1),
                    new TreeNode(1,-1,-1,0),
                },
                1,
                new TreeNode()
            },
            new object[]
            { "C",
                new List<TreeNode>
                {
                    new TreeNode(0,1,-1,-1),
                    new TreeNode(1,2,-1,0),
                    new TreeNode(2,-1,-1,1),
                },
                2,
                new TreeNode(0,1,-1,-1)
            },
        };
        #endregion


        [Test, TestCaseSource(nameof(Add))]
        public void AddNewTreeNode(string caseName, List<object[]> input, List<TreeNode> expected)
        {
            var program = new Program();
            program.AddRawInputToList(input[0]);
            program.ExecuteQueries();

            var treeNode = program.TreeNodes[0];
            treeNode.Value.ShouldBe(expected[0].Value);
            treeNode.LeftChildIndex.ShouldBe(expected[0].LeftChildIndex);
            treeNode.RightChildIndex.ShouldBe(expected[0].RightChildIndex);
            treeNode.ParentIndex.ShouldBe(expected[0].ParentIndex);
        }

        #region
        private static readonly object[] Add =
        {
                new object[] { "A", new List<object[]>
                                    {
                                        new object[] { "+", 1, -1 }
                                    },
                                    new List<TreeNode>
                                    {
                                        new TreeNode(1,-1,-1,-1)
                                    }
                }
        };
        #endregion

        [Test, TestCaseSource(nameof(Raw))]
        public void TransformArrayIntoInputTriple_Tests(string caseName, object[] input,
            List<InputTriple> expected)
        {
            var program = new Program();
            program.AddRawInputToList(input);

            var operation = (string)input[0];
            var low = (int)input[1];
            var high = -1;
            if (input.Length == 3)
                high = (int)input[2];

            var triple = program.Queries[0];
            triple.Operation.ShouldBe(operation);
            triple.Low.ShouldBe(low);
            triple.High.ShouldBe(high);
        }

        #region
        private static readonly object[] Raw =
        {
                new object[] { "A", new object[] {"+",1},  new List<InputTriple> {
                        new InputTriple("+", 1, -1)
                    }
                },
                new object[] { "B", new object[] {"?",5},  new List<InputTriple> {
                        new InputTriple("?", 5, -1)
                    }
                },
                new object[] { "C", new object[] {"s",1,2},  new List<InputTriple> {
                        new InputTriple("s", 1, 2)
                    }
                },
                new object[] { "D", new object[] {"-",1},  new List<InputTriple> {
                        new InputTriple("-", 1, -1)
                    }
                },
                new object[] { "E", new object[] {"s",999999999,1000000000},  new List<InputTriple> {
                        new InputTriple("s",999999999,1000000000)
                    }
                },
        };
        #endregion
    }
}
