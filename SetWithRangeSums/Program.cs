﻿using System;
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
            if (inputNode.Parent == -1)
                return;

            switch (DetermineZigZigZag(inputNode))
            {
                case "zig left":
                    ZigLeft(inputNode);
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

        internal void ZigLeft(TreeNode nodeToSplay)
        {
            // left case
            var parentNode = TreeNodes[nodeToSplay.Parent ?? -1];
            var tempParentValue = parentNode.Value;
            var tempParentLeftChildIndex = parentNode.LeftChild;
            var tempParentRightChildIndex = parentNode.RightChild;
            var tempParentParentIndex = parentNode.Parent;

            var formerParent = TreeNodes[nodeToSplay.Parent ?? -1];
            formerParent.Value = nodeToSplay.Value;
            formerParent.LeftChild = nodeToSplay.LeftChild;
            formerParent.RightChild = nodeToSplay.RightChild;
            formerParent.Parent = nodeToSplay.Parent;

            nodeToSplay.Value = tempParentValue;
            nodeToSplay.LeftChild = tempParentLeftChildIndex;
            nodeToSplay.RightChild = tempParentRightChildIndex;
            nodeToSplay.Parent = tempParentParentIndex;

            // place subtrees in the right places
            return;
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
            TreeNodes.Add(new TreeNode(value, -1, -1, -1));
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

        internal TreeNode GetGrandparentNode(TreeNode splayNode)
        {
            if (splayNode.Parent == -1)
                return new TreeNode();

            var parentNode = TreeNodes[splayNode.Parent ?? -1];
            if (parentNode.Parent == -1)
                return new TreeNode();

            var grandparentNode = TreeNodes[parentNode.Parent ?? -1];
            return grandparentNode;
        }

        internal string DetermineZigZigZag(TreeNode splayNode)
        {
            if (splayNode.Parent == -1)
                return "none";

            var splayNodeGrandparent = GetGrandparentNode(splayNode);
            var parentNode = TreeNodes[splayNode.Parent ?? -1];
            if (!splayNodeGrandparent.Value.HasValue)
            {
                if (parentNode.LeftChild != -1 && 
                    TreeNodes[parentNode.LeftChild ?? -1] == splayNode)
                    return "zig left";
                else
                    return "zig right";
            }

            var zigZigZag = "Hi. Something went wrong.";

            if (splayNodeGrandparent.Value.HasValue)
            {
                var parentLeftRight = "left";
                if (parentNode.RightChild != -1
                    && TreeNodes[parentNode.RightChild ?? -1] == splayNode)
                    parentLeftRight = "right";

                var grandparentLeftRight = "left";
                if (splayNodeGrandparent.RightChild != -1
                    && TreeNodes[splayNodeGrandparent.RightChild ?? -1] == parentNode)
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
    }

    public class TreeNode
    {
        public int Value { get; set; }
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
