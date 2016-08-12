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
            if (inputNode.ParentIndex == -1)
                return;

            // Determine case: zig, zig-zag, zig-zig

            // zig
            // if nodeToSplay does not have a grandparent, then zig


        }

        internal void ExecuteQueries()
        {
            var operation = Queries[0].Operation;
            var low = Queries[0].Low;

            switch (operation)
            {
                case "+" :
                    AddNode(low);
                    break;
            }
        }

        private void AddNode(int value)
        {
            TreeNodes.Add(new TreeNode(value,-1,-1,-1));
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
            if (splayNode.ParentIndex == -1)
                return new TreeNode();

            var parentNode = TreeNodes[splayNode.ParentIndex ?? -1];
            if (parentNode.ParentIndex == -1)
                return new TreeNode();

            var grandparentNode = TreeNodes[parentNode.ParentIndex ?? -1];
            return grandparentNode;
        }

        internal string DetermineZigZigZag(TreeNode splayNode)
        {
            if (splayNode.ParentIndex == -1)
                return "none";

            var splayNodeGrandparent = GetGrandparentNode(splayNode);
            if (!splayNodeGrandparent.Value.HasValue)
                return "zig";

            var zigZigZag = "hi";
            var parentNode = TreeNodes[splayNode.ParentIndex ?? -1];
            if (splayNodeGrandparent.Value.HasValue)
            {
                var parentLeftRight = "left";
                if (parentNode.RightChildIndex != -1 
                    && TreeNodes[parentNode.RightChildIndex ?? -1] == splayNode)
                    parentLeftRight = "right";

                var grandparentLeftRight = "left";
                if (splayNodeGrandparent.RightChildIndex != -1 
                    && TreeNodes[splayNodeGrandparent.RightChildIndex ?? -1] == parentNode)
                    grandparentLeftRight = "right";

                if (parentLeftRight == "left" && grandparentLeftRight == "left" 
                    || parentLeftRight == "right" && grandparentLeftRight == "right")
                    zigZigZag = "zigzig";
                else
                    zigZigZag = "zigzag";
            }
                
            return zigZigZag;
        }
    }

    public class TreeNode
    {
        public int? Value { get; set; }
        public int? LeftChildIndex { get; set; }
        public int? RightChildIndex { get; set; }
        public int? ParentIndex { get; set; }

        public TreeNode()
        {
            Value = null;
            LeftChildIndex = null;
            RightChildIndex = null;
            ParentIndex = null;
        }

        public TreeNode(int val, int left, int right, int parent)
        {
            Value = val;
            LeftChildIndex = left;
            RightChildIndex = right;
            ParentIndex = parent;
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
