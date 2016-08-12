using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;

namespace SetWithRangeSums
{
    [TestFixture]
    class aaaSetWithRangeSums
    {
        [Test]
        public void ZigLeft_A()
        {
            var program = new Program();
            var splayNode = new TreeNode(1, null, null, null);
            var parentNode = new TreeNode(0, splayNode, null, null);
            splayNode.Parent = parentNode;

            program.ZigLeft(splayNode);

            splayNode.Value.ShouldBe(1);
            splayNode.LeftChild.ShouldBe(parentNode);
            splayNode.RightChild.ShouldBeNull();
            splayNode.Parent.ShouldBeNull();

            parentNode.Value.ShouldBe(0);
            parentNode.LeftChild.ShouldBeNull();
            parentNode.RightChild.ShouldBeNull();
            parentNode.Parent.ShouldBe(splayNode);
        }

        [Test]
        public void DetermineZigZag_None()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe("none");
        }

        [Test]
        public void DetermineZigZag_Zig_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, splayNode, null, null);
            splayNode.Parent = parentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe("zig left");
        }

        [Test]
        public void DetermineZigZag_Zig_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, null, splayNode, null);
            splayNode.Parent = parentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe("zig right");
        }

        [Test]
        public void DetermineZigZag_ZigZig_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, splayNode, null, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, parentNode, null, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe("zigzig left");
        }

        [Test]
        public void DetermineZigZag_ZigZig_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, null, splayNode, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, null, parentNode, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe("zigzig right");
        }

        [Test]
        public void DetermineZigZag_Zig_Zag_Right()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, null, splayNode, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, parentNode, null, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe("zigzag right");
        }

        [Test]
        public void DetermineZigZag_Zig_Zag_Left()
        {
            var program = new Program();
            var splayNode = new TreeNode(0, null, null, null);
            var parentNode = new TreeNode(1, splayNode, null, null);
            splayNode.Parent = parentNode;
            var grandparentNode = new TreeNode(2, null, parentNode, null);
            parentNode.Parent = grandparentNode;

            var zigZag = program.DetermineZigZigZag(splayNode);
            zigZag.ShouldBe("zigzag left");
        }

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
