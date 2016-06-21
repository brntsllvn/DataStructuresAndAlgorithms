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

            var descendingFirst = first.OrderByDescending(x => x).ToArray();
            var ascendingSecond = second.OrderBy(x => x).ToArray();

            var descendingFirstLessOne = new int[descendingFirst.Length - 1];
            for (int i = 0; i < descendingFirst.Length - 1; i++)
                descendingFirstLessOne[i] = descendingFirst[i+1];

            var ascendingSecondLessOne = new int[ascendingSecond.Length - 1];
            for (int i = 0; i < ascendingSecond.Length - 1; i++)
                ascendingSecondLessOne[i] = ascendingSecond[i+1];

            var maxMinProduct = descendingFirst[0] * ascendingSecond[0];
            var result = maxMinProduct + ComputeMinDotProduct(descendingFirstLessOne, ascendingSecondLessOne);

            return result;
        }
    }
}
