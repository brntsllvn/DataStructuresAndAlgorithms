//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace TreeHeight
//{
//    internal class TreeHeight
//    {
//        public int N { get; set; }
//        public int[] Parent { get; set; }

//        public TreeHeight(int n, int[] parent)
//        {
//            N = n;
//            Parent = parent;
//        }

//        public int CalculateTreeHeight()
//        {
//            if (Parent.Length == 0)
//                return 0;

//            var q = new List<int>();
//            q.Add(Array.IndexOf(Parent,-1));

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

//                    q.AddRange(GetChildren(newnode));

//                    nodeCount--;
//                }
//            }
//        }

//        public List<int> GetChildren(int index)
//        {
//            var childrenLocations = Enumerable
//                .Range(0, Parent.Length)
//                .Where(k => Parent[k] == index)
//                .ToList();
//            return childrenLocations;
//        } 
//    }
//}

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        new Launcher().Run(args);
//    }
//}

//public class Launcher
//{
//    public void Run(string[] args)
//    {
//        var numVertices = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray()[0];
//        var parentCoordinates = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
//        var tree = new TreeHeight.TreeHeight(numVertices, parentCoordinates);
//        Console.WriteLine(tree.CalculateTreeHeight());
//    }
//}
