using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace SetWithRangeSums
{
    public class Program
    {
        public List<QueryTriple> Queries { get; set; }
        public List<TreeNode> TreeNodes { get; set; }
        public int RootNodeIndex { get; set; }
        public List<string> QueryResults { get; set; }

        public Program()
        {
            Queries = new List<QueryTriple>();
            TreeNodes = new List<TreeNode>();
            QueryResults = new List<string>();
            RootNodeIndex = 0;
        }

        internal void ExecuteQueries()
        {
            foreach (var query in Queries)
            {
                var operation = query.Operation;
                var operand = query.Low;
                TreeNode root;
                switch (operation)
                {
                    case Operations.Add:
                        if (!TreeNodes.Any())
                        {
                            TreeNodes.Add(new TreeNode(operand));
                            break;
                        }
                        root = TreeNodes[RootNodeIndex];
                        Add(operand, root);
                        break;

                        // implement splay add

                    case Operations.Find:
                        if (!TreeNodes.Any())
                        {
                            QueryResults.Add(Results.NotFound);
                            break;
                        }
                        root = TreeNodes[RootNodeIndex];
                        var foundNode = Find(operand, root);
                        if (foundNode.Value != operand)
                            QueryResults.Add(Results.NotFound);
                        QueryResults.Add(Results.Found);
                        break;

                    // implement splay find

                    case Operations.Del:
                        if (!TreeNodes.Any())
                            break;
                        root = TreeNodes[RootNodeIndex];
                        Del(operand, root);
                        break;
                }
            }
        }

        private void Del(int deleteTerm, TreeNode root)
        {
            var nodeToDelete = Find(deleteTerm, root);
            var parent = nodeToDelete.Parent;
            var rightChild = nodeToDelete.RightChild;
            var leftChild = nodeToDelete.LeftChild;

            TreeNodes.Remove(nodeToDelete);

            if (rightChild == null && leftChild == null)
                return;

            if (rightChild == null)
            {
                if (parent.LeftChild == nodeToDelete)
                    parent.LeftChild = leftChild;
                else
                    parent.RightChild = leftChild;
                leftChild.Parent = parent;
            }
            else
            {
                var replacementNode = Next(nodeToDelete);
                replacementNode.Parent = parent;
                parent.RightChild = replacementNode;

                if (leftChild != null)
                {
                    replacementNode.LeftChild = leftChild;
                    leftChild.Parent = replacementNode;
                }

                if (replacementNode != rightChild)
                {
                    replacementNode.RightChild = rightChild;
                    rightChild.Parent = replacementNode;
                }

            }
        }

        private TreeNode Next(TreeNode node)
        {
            if (node.RightChild != null)
            {
                var leftDescendant = LeftDescendant(node.RightChild);
                return leftDescendant;
            }
            var rightAncestor = RightAncestor(node);
            return  rightAncestor;
        }

        private TreeNode RightAncestor(TreeNode node)
        {
            return node.Value < node.Parent.Value ? node.Parent : RightAncestor(node.Parent);
        }

        private TreeNode LeftDescendant(TreeNode node)
        {
            return node.LeftChild == null ? node : LeftDescendant(node.LeftChild);
        }

        internal void Add(int insertionTerm, TreeNode root)
        {
            var parent = Find(insertionTerm, root);

            // no duplicate entries
            if (parent.Value == insertionTerm)
                return;

            var newNode = new TreeNode(insertionTerm, null, null, parent);
            TreeNodes.Add(newNode);
            if (insertionTerm < parent.Value && parent.LeftChild == null) 
                parent.LeftChild = newNode;
            else
                parent.RightChild = newNode;
        }

        internal TreeNode Find(int searchTerm, TreeNode root)
        {
            var rootVal = root.Value;
            if (rootVal == searchTerm)
                return root;

            if (rootVal > searchTerm)
                return root.LeftChild != null ? Find(searchTerm, root.LeftChild) : root;
            return root.RightChild != null ? Find(searchTerm, root.RightChild) : root;
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

        internal void AddRawInputToList(object[] input)
        {
            var operation = (string)input[0];
            var low = (int)input[1];
            var high = -1;

            if (input.Length == 3)
                high = (int)input[2];

            Queries.Add(new QueryTriple(operation, low, high));
        }

        public void ReadData()
        {
            var numQueries = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray()[0];
            for (int i = 0; i < numQueries; i++)
            {
                var query = Console.ReadLine().Split(' ').Select(n => Convert.ToString(n)).ToArray();
                AddRawInputToList(query);
            }
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

    public static class Results
    {
        public const string Found = "Found";
        public const string NotFound = "Not found";
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

        public TreeNode(int val = -1, TreeNode left = null, TreeNode right = null, TreeNode parent = null)
        {
            Value = val;
            LeftChild = left;
            RightChild = right;
            Parent = parent;
        }
    }

    public class QueryTriple
    {
        public string Operation { get; set; }
        public int Low { get; set; }
        public int High { get; set; }

        public QueryTriple(string op, int low, int high = -1)
        {
            Operation = op;
            Low = low;
            High = high;
        }
    }
}
