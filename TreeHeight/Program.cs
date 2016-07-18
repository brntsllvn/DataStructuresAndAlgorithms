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
            var numVertices = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var parentCoordinates = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            Console.WriteLine(CalculateTreeHeight(parentCoordinates));
        }

        public string CalculateTreeHeight(long[] n)
        {
            return "hello";
        }
    }

    public class BjsTree
    {
        public BjsNode Root { get; set; }
    }

    public class BjsNode
    {
        public string Payload { get; set; }
        public List<BjsTree> ChildTrees { get; set; }

        public BjsNode(string value)
        {
            Payload = value;
        }
    }
}
