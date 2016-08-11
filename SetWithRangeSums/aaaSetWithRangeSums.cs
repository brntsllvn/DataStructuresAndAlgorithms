using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace SetWithRangeSums
{
    [TestFixture]
    class aaaSetWithRangeSums
    {
        [Test, TestCaseSource(nameof(Splay))]
        public void SplayNode_Test(string caseName, TreeNode inputNode, TreeNode expectedNode)
        {
            var program = new Program();
            program.Splay(inputNode);

            inputNode.Value.ShouldBe(expectedNode.Value);
            inputNode.LeftChildIndex.ShouldBe(expectedNode.LeftChildIndex);
            inputNode.RightChildIndex.ShouldBe(expectedNode.RightChildIndex);
            inputNode.ParentIndex.ShouldBe(expectedNode.ParentIndex);
        }

        #region
        public static object[] Splay =
        {
            new object[] { "A", new TreeNode(1,-1,-1,-1), new TreeNode(1,-1,-1,-1) }
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
