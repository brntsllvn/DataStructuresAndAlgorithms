using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeHeight
{
    class TreeHeight
    {
        public int N { get; set; }
        public int[] Parent { get; set; }

        public TreeHeight(int n, int[] parent)
        {
            N = n;
            Parent = parent;
        }

        public int ComputeHeight()
        {
            var maxHeight = 0;
            for (int vertex = 0; vertex < N; vertex++)
            {
                var height = 0;
                for (int i = vertex; i != -1; i = Parent[i])
                {
                    height++;
                }
                maxHeight = Math.Max(maxHeight, height);
            }
            return maxHeight;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        new TreeHeight.Launcher().Run(args);
    }
}

public class Launcher
{
    public void Run(string[] args)
    {
        var numVertices = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray()[0];
        var parentCoordinates = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        var tree = new TreeHeight.TreeHeight(numVertices, parentCoordinates);
        Console.WriteLine(tree.ComputeHeight());
    }
}
