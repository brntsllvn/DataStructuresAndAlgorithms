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
            Console.WriteLine(CalculateTreeHeight(tree));
        }

        private List<TreeNode> ConstructTreeFromArray(int[] parentCoordinates)
        {
            //  0 1 2 3 4
            // -1 0 0 1 2

            var numNodes = parentCoordinates.Length;

            var tree = new List<TreeNode>();
            for (int i = 0; i < numNodes; i++)
                tree.Add(new TreeNode(i));

            for (int j = 0; j < numNodes; j++)
            {
                // set parent
                TreeNode parentNode = null;
                if (parentCoordinates[j] > -1)
                    parentNode = tree.ElementAt(parentCoordinates[j]);
                tree[j].Parent = parentNode;

                // set children
                // return all nodes with parent = j
                var childrenLocations = Enumerable
                    .Range(0, parentCoordinates.Length)
                    .Where(k => parentCoordinates[k] == j)
                    .ToList();

                foreach (var childLocation in childrenLocations)
                {
                    // werid error happening down here...
                    tree[j].Children.Add(j, tree.ElementAt(childLocation));
                }
            }

            return tree;
        }

        public int CalculateTreeHeight(List<TreeNode> tree)
        {
            if (tree.Count == 0)
                return 0;

            if (tree.Count == 1)
                return 1;

            var treeHeight = -1; 
            return treeHeight;
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

        public int Count
        {
            get { return Children.Count; }
        }
    }
}
