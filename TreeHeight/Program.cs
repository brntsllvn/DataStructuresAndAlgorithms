using System;
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
            Console.WriteLine(CalculateTreeHeight(parentCoordinates));
        }

        public int CalculateTreeHeight(int[] n)
        {
            if (n.Length == 0)
                return 0;

            if (n.Length == 1)
                return 1;

            // I don't like constructing the tree on every iteration...
            var root = ConstructTree(n);

            var subTreeHeights = new List<int>();

            // start with root
            // loop through each child and calculate height
            foreach (var childNode in root.ChildNodes)
            {
                // need to trim tree to use recursion
                subTreeHeights = CalculateTreeHeight();
            }

            // take max iteratively

            // measure tree height
            var treeHeight = 1; 
            return treeHeight;
        }

        public BjsNode ConstructTree(int[] n)
        {
            // construct tree nodes from array
            var tree = new BjsNode[n.Length];
            BjsNode root = null;
            for (int i = 0; i < n.Length; i++)
            {
                tree[i] = new BjsNode(i) {Payload = i};
                if (tree[i].Payload == -1)
                    root = tree[i];
                else
                {
                    // !!! this is wrong...but makes the code compile !!!
                    root = tree[0]; 
                }
            }

            // populate child associations
            for (int j = 0; j < n.Length; j++)
            {
                for (int k = 0; k < n.Length; k++)
                {
                    if (k != j && tree[k].Payload == j)
                    {
                        tree[j].ChildNodes.Add(tree[k]);
                    }
                }
            }
            return root;
        }
    }

    public class BjsNode
    {
        public int Payload { get; set; }
        public List<BjsNode> ChildNodes { get; set; }

        public BjsNode(int value)
        {
            Payload = value;
        }
    }
}
