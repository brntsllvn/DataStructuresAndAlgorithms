using System;
using System.Collections.Generic;
using System.Linq;

namespace SetWithRangeSums
{
    public class Program
    {
        public List<InputTriple> Queries { get; set; }
        public List<TreeNode> TreeNodes { get; set; }
        public List<string> QueryResults { get; set; }

        public Program()
        {
            Queries = new List<InputTriple>();
            TreeNodes = new List<TreeNode>();
            QueryResults = new List<string>();
        }

        public void Splay(TreeNode inputNode)
        {
            if (inputNode.Parent.Value == -1)
                return;

            switch (DetermineZigZigZag(inputNode))
            {
                case "zig left":
                    ZigLeft(inputNode);
                    break;
                case "zig right":
                    ZigRight(inputNode);
                    break;
                case "zigzig":
                    // don't forget to reassign great-gransparent pointers
                    break;
                case "zigzag":
                    // don't forget to reassign great-gransparent pointers
                    break;
                default:
                    break;
            }
        }

        internal void ZigZigLeft(TreeNode splayNode)
        {
            var splayNodeRightChild = splayNode.RightChild;
            var parent = splayNode.Parent;
            var parentRightChild = parent.RightChild;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.RightChild = parent;
            splayNode.Parent = null;

            parent.LeftChild = splayNodeRightChild;

            if (parentRightChild != null)
            {
                parentRightChild.Parent = grandparent;
                grandparent.LeftChild = parentRightChild;
            }

            parent.RightChild = grandparent;
            parent.Parent = splayNode;

            if (splayNodeRightChild != null)
                splayNodeRightChild.Parent = parent;

            grandparent.Parent = parent;


            if (greatGrandparent != null)
                greatGrandparent.LeftChild = splayNode;
        }

        internal void ZigLeft(TreeNode splayNode)
        {
            var parentNode = splayNode.Parent;
            parentNode.LeftChild = splayNode.RightChild;
            parentNode.Parent = splayNode;

            var splayNodeRightChild = splayNode.RightChild;
            if (splayNodeRightChild != null)
                splayNodeRightChild.Parent = parentNode;

            splayNode.RightChild = parentNode;
            splayNode.Parent = null;
        }

        internal void ZigRight(TreeNode splayNode)
        {
            var parentNode = splayNode.Parent;
            parentNode.RightChild = splayNode.LeftChild;
            parentNode.Parent = splayNode;

            var splayNodeLeftChild = splayNode.LeftChild;
            if (splayNodeLeftChild != null)
                splayNodeLeftChild.Parent = parentNode;

            splayNode.LeftChild = parentNode;
            splayNode.Parent = null;
        }

        internal void ExecuteQueries()
        {
            var operation = Queries[0].Operation;
            var low = Queries[0].Low;

            switch (operation)
            {
                case "+":
                    AddNode(low);
                    break;
            }
        }

        private void AddNode(int value)
        {
        }

        internal void AddRawInputToList(object[] input)
        {
            var operation = (string)input[0];
            var low = (int)input[1];
            var high = -1;

            if (input.Length == 3)
                high = (int)input[2];

            Queries.Add(new InputTriple(operation, low, high));
        }

        public void ReadData()
        {
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
        }

        public void WriteResponse()
        {
            Console.WriteLine();
        }

        public void Run()
        {
            ReadData();
            ExecuteQueries();
            WriteResponse();
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }

        public string DetermineZigZigZag(TreeNode splayNode)
        {
            if (splayNode.Parent == null)
                return "none";

            var zigZigZag = "hi, something went wrong";

            var parentNode = splayNode.Parent;
            if (!NodeHasGrandparent(splayNode))
            {
                if (parentNode.LeftChild == splayNode)
                    return "zig left";
                else
                    return "zig right";
            }
            else
            {
                var grandparentNode = GetGrandparentNode(splayNode);

                var parentLeftRight = "left";
                if (parentNode.RightChild == splayNode)
                    parentLeftRight = "right";

                var grandparentLeftRight = "left";
                if (grandparentNode.RightChild == parentNode)
                    grandparentLeftRight = "right";

                if (parentLeftRight == "left" && grandparentLeftRight == "left")
                    zigZigZag = "zigzig left";
                else if (parentLeftRight == "right" && grandparentLeftRight == "right")
                    zigZigZag = "zigzig right";
                else if (parentLeftRight == "left" && grandparentLeftRight == "right")
                    zigZigZag = "zigzag left";
                else
                    zigZigZag = "zigzag right";
            }

            return zigZigZag;
        }

        private static TreeNode GetGrandparentNode(TreeNode parentNode)
        {
            return parentNode.Parent.Parent;
        }

        public bool NodeHasGrandparent(TreeNode node)
        {
            return node.Parent != null && node.Parent.Parent != null;
        }
    }

    public class TreeNode
    {
        public int? Value { get; set; }
        public TreeNode LeftChild { get; set; }
        public TreeNode RightChild { get; set; }
        public TreeNode Parent { get; set; }

        public TreeNode()
        {
            Value = -1;
            LeftChild = null;
            RightChild = null;
            Parent = null;
        }

        public TreeNode(int val, TreeNode left, TreeNode right, TreeNode parent)
        {
            Value = val;
            LeftChild = left;
            RightChild = right;
            Parent = parent;
        }
    }

    public class InputTriple
    {
        public string Operation { get; set; }
        public int Low { get; set; }
        public int High { get; set; }

        public InputTriple(string op, int val)
        {
            Operation = op;
            Low = val;
            High = -1;
        }

        public InputTriple(string op, int low, int high)
        {
            Operation = op;
            Low = low;
            High = high;
        }
    }
}
