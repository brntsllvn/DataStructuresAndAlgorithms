using System;
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
            var solution = new HeightOfTreeFromParentArray();

            var numVertices = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            var parent = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

            Console.WriteLine(solution.ComputeHeight(parent));
        }
    }

    public class HeightOfTreeFromParentArray
    {
        public void ComputeHeight(int i, int[] height, int[] parent)
        {
            if (height[i] != -1)
                return;

            if (parent[i] == -1)
            {
                height[i] = 1;
                return;
            }

            if (height[parent[i]] == -1)
                ComputeHeight(parent[i], height, parent);

            if (height[parent[i]] != -1) 
                height[i] = 1 + height[parent[i]];
        }

        public int ComputeHeight(int[] parent)
        {
            var height = new int[parent.Length];

            for (var i = 0; i < height.Length; i++)
                height[i] = -1;

            for (var i = 0; i < height.Length; i++)
                ComputeHeight(i, height, parent);

            var maxHeight = -1;
            for (var i = 0; i < height.Length; i++)
            {
                if (height[i] > maxHeight)
                    maxHeight = height[i];
            }

            return maxHeight;
        }
    }
}

