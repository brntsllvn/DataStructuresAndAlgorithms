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
            var firstSequence = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var secondSequence = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            Console.WriteLine(ComputeMinDotProduct(firstSequence, secondSequence));
        }

        public long ComputeMinDotProduct(long[] first, long[] second)
        {
            var descendingFirst = first.OrderByDescending(x => x).ToArray();
            var ascendingSecond = second.OrderBy(x => x).ToArray();

            if (descendingFirst.Length == 1)
                return descendingFirst[0] * ascendingSecond[0];

            var descendingFirstLessOne = new long[descendingFirst.Length - 1];
            for (long i = 0; i < descendingFirst.Length - 1; i++)
                descendingFirstLessOne[i] = descendingFirst[i+1];

            var ascendingSecondLessOne = new long[ascendingSecond.Length - 1];
            for (long i = 0; i < ascendingSecond.Length - 1; i++)
                ascendingSecondLessOne[i] = ascendingSecond[i+1];

            var maxMinProduct = descendingFirst[0] * ascendingSecond[0];
            var result = maxMinProduct + ComputeMinDotProduct(descendingFirstLessOne, ascendingSecondLessOne);

            return result;
        }
    }
}
