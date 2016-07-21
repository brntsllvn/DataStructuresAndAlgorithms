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

            Console.WriteLine(solution.CalculateTreeHeight(parent));
        }
    }

    public class HeightOfTreeFromParentArray
    {
        public void CalculateNodeHeight(int i, int[] heightOfNode, int[] parentCoordinates)
        {
            if (heightOfNode[i] != -1)
                return;

            if (parentCoordinates[i] == -1)
            {
                heightOfNode[i] = 1;
                return;
            }

            // if the height of node i's parent has NOT been calculated, then calculate it
            if (heightOfNode[parentCoordinates[i]] == -1)
                CalculateNodeHeight(parentCoordinates[i], heightOfNode, parentCoordinates);

            // if the height of node i's parent HAS been calculated, the height of i is 1 + parent's height
            if (heightOfNode[parentCoordinates[i]] != -1) 
                heightOfNode[i] = 1 + heightOfNode[parentCoordinates[i]];
        }

        public int CalculateTreeHeight(int[] parentCoordinates)
        {
            var heightOfNode = new int[parentCoordinates.Length];

            for (var i = 0; i < heightOfNode.Length; i++)
                heightOfNode[i] = -1;

            for (var i = 0; i < heightOfNode.Length; i++)
                CalculateNodeHeight(i, heightOfNode, parentCoordinates);

            var maxHeight = -1;
            for (var i = 0; i < heightOfNode.Length; i++)
                if (heightOfNode[i] > maxHeight)
                    maxHeight = heightOfNode[i];

            return maxHeight;
        }
    }
}

