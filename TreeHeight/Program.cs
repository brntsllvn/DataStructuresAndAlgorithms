using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TreeHeight
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Launcher().Run(args);
        }
    }

    public class Launcher
    {
        public void Run(string[] args)
        {
            var numVertices = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            var parentCoordinates = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            var tree = ConstructTreeFromArray(parentCoordinates);
            var root = tree.FirstOrDefault(node => node.Parent == null);
            Console.WriteLine(CalculateTreeHeight(root));
        }

        public int CalculateTreeHeight(TreeNode node)
        {
            if (node == null)
                return 0;

            var q = new List<TreeNode>();
            q.Add(node);

            int height = 0;

            while (true)
            {
                int nodeCount = q.Count;
                if (nodeCount == 0)
                    return height;

                height++;

                while (nodeCount > 0)
                {
                    var newnode = q.First();
                    q.RemoveAt(0);

                    q.AddRange(newnode.GetAllChildren());

                    nodeCount--;
                }
            }
        }

        public List<TreeNode> ConstructTreeFromArray(int[] parentCoordinates)
        {
            var numNodes = parentCoordinates.Length;

            var tree = new List<TreeNode>();
            for (int i = 0; i < numNodes; i++)
                tree.Add(new TreeNode(i));

            if (parentCoordinates.Length == 1)
                return tree;

            for (int j = 0; j < numNodes; j++)
            {
                TreeNode parentNode = null;
                if (parentCoordinates[j] > -1)
                    parentNode = tree.ElementAt(parentCoordinates[j]);
                tree[j].Parent = parentNode;

                var childrenLocations = Enumerable
                    .Range(0, parentCoordinates.Length)
                    .Where(k => parentCoordinates[k] == j)
                    .ToList();

                foreach (var childLocation in childrenLocations)
                    tree[j].Children.Add(childLocation, tree.ElementAt(childLocation));
            }

            return tree;
        }
    }

    public class TreeNode : IEnumerable<TreeNode>
    {
        public Dictionary<int, TreeNode> Children = new Dictionary<int, TreeNode>();
        public int ID;
        public TreeNode Parent { get; set; }

        public TreeNode(int id)
        {
            ID = id;
        }

        public TreeNode GetChild(int id)
        {
            return Children[id];
        }

        public List<TreeNode> GetAllChildren()
        {
            return Children.Values.ToList();
        }

        public void Add(TreeNode node)
        {
            if (node.Parent != null)
                node.Parent.Children.Remove(node.ID);

            node.Parent = this;
            Children.Add(node.ID, node);
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            return Children.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //public int CountChildren
        //{
        //    get { return Children.Count; }
        //}
    }
}
