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

        public int CalculateTreeHeight(List<TreeNode> tree)
        {
            if (tree.Count == 0)
                return 0;

            if (tree.Count == 1)
                return 1;

            var root = tree.FirstOrDefault(node => node.Parent == null) ?? tree.FirstOrDefault().Parent;

            var rootChildren = root.Children;

            var childrenTreeHeights = new List<int>();

            foreach (var child in rootChildren)
            {
                var childrenChildren = new List<TreeNode>();
                childrenChildren = child.Value.GetAllChildren();
                childrenTreeHeights.Add(CalculateTreeHeight(childrenChildren));
            }

            var treeHeight = childrenTreeHeights.Max(x => x); 
            return treeHeight;
        }

        public List<TreeNode> ConstructTreeFromArray(int[] parentCoordinates)
        {
            var numNodes = parentCoordinates.Length;

            var tree = new List<TreeNode>();
            for (int i = 0; i < numNodes; i++)
                tree.Add(new TreeNode(i));

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
