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
            switch (DetermineZigZigZag(inputNode))
            {
                case ZiggaZigAh.ZigLeft:
                    ZigLeft(inputNode);
                    break;
                case ZiggaZigAh.ZigRight:
                    ZigRight(inputNode);
                    break;
                case ZiggaZigAh.ZigZigLeft:
                    ZigZigLeft(inputNode);
                    break;
                case ZiggaZigAh.ZigZigRight:
                    ZigZigRight(inputNode);
                    break;
                case ZiggaZigAh.ZigZagLeft:
                    ZigZagLeft(inputNode);
                    break;
                case ZiggaZigAh.ZigZagRight:
                    ZigZagRight(inputNode);
                    break;
            }

            if (inputNode.Parent != null)
                Splay(inputNode);
        }

        internal void ZigZagLeft(TreeNode splayNode)
        {
            var splayLeftChild = splayNode.LeftChild;
            var splayRightChild = splayNode.RightChild;
            var parent = splayNode.Parent;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.Parent = null;
            if (greatGrandparent != null)
            {
                splayNode.Parent = greatGrandparent;

                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;
            }

            splayNode.LeftChild = parent;
            splayNode.RightChild = grandparent;

            parent.Parent = splayNode;
            parent.RightChild = null;
            if (splayLeftChild != null)
            {
                parent.RightChild = splayLeftChild;
                splayLeftChild.Parent = parent;
            }

            grandparent.Parent = splayNode;
            grandparent.LeftChild = null;
            if (splayRightChild != null)
            {
                grandparent.LeftChild = splayRightChild;
                splayRightChild.Parent = grandparent;
            }
        }

        internal void ZigZagRight(TreeNode splayNode)
        {
            var splayLeftChild = splayNode.LeftChild;
            var splayRightChild = splayNode.RightChild;
            var parent = splayNode.Parent;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.Parent = null;
            if (greatGrandparent != null)
            {
                splayNode.Parent = greatGrandparent;
                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;
            }

            splayNode.LeftChild = grandparent;
            splayNode.RightChild = parent;

            parent.Parent = splayNode;
            parent.LeftChild = null;
            if (splayRightChild != null)
            {
                parent.LeftChild = splayRightChild;
                splayRightChild.Parent = parent;
            }

            grandparent.Parent = splayNode;
            grandparent.RightChild = null;
            if (splayLeftChild != null)
            {
                grandparent.RightChild = splayLeftChild;
                splayLeftChild.Parent = grandparent;
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
            else
                grandparent.LeftChild = null;

            parent.RightChild = grandparent;
            parent.Parent = splayNode;

            if (splayNodeRightChild != null)
                splayNodeRightChild.Parent = parent;

            grandparent.Parent = parent;

            if (greatGrandparent != null)
            {
                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;

                splayNode.Parent = greatGrandparent;
            }
        }

        internal void ZigZigRight(TreeNode splayNode)
        {
            var splayNodeLeftChild = splayNode.LeftChild;
            var parent = splayNode.Parent;
            var parentLeftChild = parent.LeftChild;
            var grandparent = parent.Parent;
            var greatGrandparent = grandparent.Parent;

            splayNode.LeftChild = parent;
            splayNode.Parent = null;

            parent.RightChild = splayNodeLeftChild;

            grandparent.RightChild = null;
            if (parentLeftChild != null)
            {
                parentLeftChild.Parent = grandparent;
                grandparent.RightChild = parentLeftChild;
            }
            else
                grandparent.LeftChild = null;

            parent.LeftChild = grandparent;
            parent.Parent = splayNode;

            if (splayNodeLeftChild != null)
                splayNodeLeftChild.Parent = parent;

            grandparent.Parent = parent;

            if (greatGrandparent != null)
            {
                if (greatGrandparent.LeftChild == grandparent)
                    greatGrandparent.LeftChild = splayNode;
                else
                    greatGrandparent.RightChild = splayNode;

                splayNode.Parent = greatGrandparent;
            }
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
            //var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();

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

        static void Main()
        {
            new Program().Run();
        }

        public string DetermineZigZigZag(TreeNode splayNode)
        {
            if (splayNode.Parent == null)
                return "none";

            string zigZigZag;

            var parentNode = splayNode.Parent;
            if (!NodeHasGrandparent(splayNode))
            {
                if (parentNode.LeftChild == splayNode)
                    return ZiggaZigAh.ZigLeft;
                return ZiggaZigAh.ZigRight;
            }
            var grandparentNode = GetGrandparentNode(splayNode);

            var splayNodeIsWhichChildOfParent = "left";
            if (parentNode.RightChild == splayNode)
                splayNodeIsWhichChildOfParent = "right";

            var parentNodeIsWhichChildOfParent = "left";
            if (grandparentNode.RightChild == parentNode)
                parentNodeIsWhichChildOfParent = "right";

            if (splayNodeIsWhichChildOfParent == "left" && parentNodeIsWhichChildOfParent == "left")
                zigZigZag = ZiggaZigAh.ZigZigLeft;
            else if (splayNodeIsWhichChildOfParent == "right" && parentNodeIsWhichChildOfParent == "right")
                zigZigZag = ZiggaZigAh.ZigZigRight;
            else if (splayNodeIsWhichChildOfParent == "left" && parentNodeIsWhichChildOfParent == "right")
                zigZigZag = ZiggaZigAh.ZigZagRight;
            else
                zigZigZag = ZiggaZigAh.ZigZagLeft;
            return zigZigZag;
        }

        private static TreeNode GetGrandparentNode(TreeNode parentNode)
        {
            return parentNode.Parent.Parent;
        }

        public bool NodeHasGrandparent(TreeNode node)
        {
            return node.Parent?.Parent != null;
        }
    }

    public static class ZiggaZigAh
    {
        public const string ZigLeft = "zig left";
        public const string ZigRight = "zig right";
        public const string ZigZigLeft = "zigzig left";
        public const string ZigZigRight = "zigzig right";
        public const string ZigZagLeft = "zigzag left";
        public const string ZigZagRight = "zigzag right";
        public const string Error = "hi, something went wrong";
        public const string None = "none";
    }

    public static class Operations
    {
        public const string Add = "+";
        public const string Del = "-";
        public const string Find = "?";
        public const string Sum = "s";
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
