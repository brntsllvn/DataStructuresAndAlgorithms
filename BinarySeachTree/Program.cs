using System;
using System.Collections.Generic;
using System.Linq;

namespace BinarySeachTree
{
    public class Program
    {
        public List<TreeNode> TreeNodes { get; set; }
        public List<long?> InOrderTraversalResult { get; set; }
        public List<long?> PreOrderTraversalResult { get; set; }
        public List<long?> PostOrderTraversalResult { get; set; }

        public Program()
        {
            TreeNodes = new List<TreeNode>();
            InOrderTraversalResult = new List<long?>();
            PreOrderTraversalResult = new List<long?>();
            PostOrderTraversalResult = new List<long?>();
        }

        public void InOrderTraversal(TreeNode root)
        {
            if (root.Key == null)
                return;

            var leftChildNode = GetLeftChildNode(root.LeftChildIndex);
            InOrderTraversal(leftChildNode);
            InOrderTraversalResult.Add(root.Key);

            var rightChildNode = GetRightChildNode(root.RightChildIndex);
            InOrderTraversal(rightChildNode);
        }

        public void PreOrderTraversal(TreeNode root)
        {
            if (root.Key == null)
                return;

            PreOrderTraversalResult.Add(root.Key);

            var leftChildNode = GetLeftChildNode(root.LeftChildIndex);
            PreOrderTraversal(leftChildNode);

            var rightChildNode = GetRightChildNode(root.RightChildIndex);
            PreOrderTraversal(rightChildNode);
        }

        public void PostOrderTraversal(TreeNode root)
        {
            if (root.Key == null)
                return;

            var leftChildNode = GetLeftChildNode(root.LeftChildIndex);
            PostOrderTraversal(leftChildNode);

            var rightChildNode = GetRightChildNode(root.RightChildIndex);
            PostOrderTraversal(rightChildNode);

            PostOrderTraversalResult.Add(root.Key);
        }

        private TreeNode GetRightChildNode(int rightChildIndex)
        {
            if (rightChildIndex == -1)
                return new TreeNode();

            return TreeNodes[rightChildIndex];
        }

        private TreeNode GetLeftChildNode(int leftChildIndex)
        {
            if (leftChildIndex == -1)
                return new TreeNode();

            return TreeNodes[leftChildIndex];
        }

        public void ReadData()
        {
            var numberOfVertices = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray()[0];
            for (int i = 0; i < numberOfVertices; i++)
            {
                var rawData = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
                AddTreeNode(rawData);
            }
        }

        public void AddTreeNode(long[] inputArray)
        {
            var key = inputArray[0];
            var leftChildIndex = inputArray[1];
            var rightChildIndex = inputArray[2];
            TreeNodes.Add(new TreeNode(key, (int)leftChildIndex, (int)rightChildIndex));
        }

        public void WriteResponse()
        {
            Console.WriteLine();
        }

        public void Run()
        {
            ReadData();
            var root = TreeNodes[0];
            PreOrderTraversal(root);
            WriteResponse();
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }

    public class TreeNode
    {
        public long? Key { get; set; }
        public int LeftChildIndex { get; set; }
        public int RightChildIndex { get; set; }

        public TreeNode()
        {
            Key = null;
            LeftChildIndex = -1;
            RightChildIndex = -1;
        }

        public TreeNode(long key, int leftChildIndex, int rightChildIndex)
        {
            Key = key;
            LeftChildIndex = leftChildIndex;
            RightChildIndex = rightChildIndex;
        }
    }
}
