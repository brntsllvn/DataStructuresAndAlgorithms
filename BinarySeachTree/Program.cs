using System;
using System.Collections.Generic;
using System.Linq;

namespace BinarySeachTree
{
    public class Program
    {
        public List<TreeNode> TreeNodes { get; set; }
        public long[] InOrderTraversalResult { get; set; }

        public Program()
        {
            TreeNodes = new List<TreeNode>();
            InOrderTraversalResult = new long[TreeNodes.Count];
        }

        public void InOrderTraversal(TreeNode root)
        {

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
            TreeNodes.Add(new TreeNode(key, leftChildIndex, rightChildIndex));
        }

        public void WriteResponse()
        {
            Console.WriteLine();
        }

        public void Run()
        {
            ReadData();
            var root = TreeNodes[0];
            InOrderTraversal(root);
            WriteResponse();
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }

    public class TreeNode
    {
        public long Key { get; set; }
        public long LeftChildIndex { get; set; }
        public long RightChildIndex { get; set; }

        public TreeNode(long key, long leftChildIndex, long rightChildIndex)
        {
            Key = key;
            LeftChildIndex = leftChildIndex;
            RightChildIndex = rightChildIndex;
        }
    }
}
