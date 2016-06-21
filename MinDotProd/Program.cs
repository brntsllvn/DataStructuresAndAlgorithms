using System;
using System.Linq;

namespace MinDotProd
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
            var numInts = Console.ReadLine();
            var firstSequence = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            var secondSequence = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            Console.WriteLine(ComputeMinDotProduct(firstSequence, secondSequence));
        }

        public int ComputeMinDotProduct(int[] first, int[] second)
        {
            if (first.Length == 1)
                return first[0] * second[0];

            return 0;
        }
    }
}
