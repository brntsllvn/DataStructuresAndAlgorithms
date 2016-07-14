using System;
using System.Linq;

namespace KnapsackWithoutRepetitions
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
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            //Console.WriteLine(KnapsackNoRep(input));
        }

        public int[] KnapsackNoRep(int knapsackCapacity, int[,] potentialItemsValuesAndWeights)
        {
            return new [] {0};
        }
    }
}
