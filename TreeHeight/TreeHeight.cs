//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace TreeHeight
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            new Launcher().Run(args);
//        }
//    }

//    public class Launcher
//    {
//        public void Run(string[] args)
//        {
//            var numVertices = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
//            var parentCoordinates = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
//            var tree = ConstructTreeFromArray(parentCoordinates);
//            var root = tree.FirstOrDefault(node => node.Parent == null);
//            Console.WriteLine(CalculateTreeHeight(root));
//        }

//        public int CalculateTreeHeight(TreeNode node)
//        {
//            if (node == null)
//                return 0;

//            var q = new List<TreeNode>();
//            q.Add(node);

//            int height = 0;

//            while (true)
//            {
//                int nodeCount = q.Count;
//                if (nodeCount == 0)
//                    return height;

//                height++;

//                while (nodeCount > 0)
//                {
//                    var newnode = q.First();
//                    q.RemoveAt(0);

//                    q.AddRange(newnode.Children.Values.ToList());

//                    nodeCount--;
//                }
//            }
//        }

//        public List<TreeNode> ConstructTreeFromArray(int[] parentCoordinates)
//        {
//            var numNodes = parentCoordinates.Length;

//            var tree = new List<TreeNode>();
//            for (var i = 0; i < numNodes; i++)
//                tree.Add(new TreeNode(i));

//            for (var j = 0; j < numNodes; j++)
//            {
//                if (parentCoordinates[j] == -1) continue;
//                tree[j].Parent = tree[parentCoordinates[j]];
//                tree[parentCoordinates[j]].Children.Add(j, tree[j]);
//            }

//            return tree;
//        }
//    }

//    public class TreeNode
//    {
//        public Dictionary<int, TreeNode> Children = new Dictionary<int, TreeNode>();
//        public int Id;
//        public TreeNode Parent { get; set; }

//        public TreeNode(int id)
//        {
//            Id = id;
//        }
//    }
//}