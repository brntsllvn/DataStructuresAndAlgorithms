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

        public int KnapsackNoRep(int knapsackCapacity, int[,] potentialItemValuesAndWeights)
        {
            var numberItems = potentialItemValuesAndWeights.GetLength(0);
            var value = new int[knapsackCapacity, numberItems];

            for (int i = 0; i < numberItems; i++)
            {
                for (int w = 1; w < knapsackCapacity; w++)
                {
                    value[w, i] = value[w, i - 1];

                    var potentialItemWeight = potentialItemValuesAndWeights[i, 1];
                    if (potentialItemWeight < knapsackCapacity)
                    {
                        var potentialItemValue = potentialItemValuesAndWeights[i, 0];
                        var tempValue = value[knapsackCapacity - w, i - 1] + potentialItemValue;
                        if (value[w,i] < tempValue)
                        {
                            value[w, i] = tempValue;
                        }
                    }
                }
            }

            return value[knapsackCapacity - 1, numberItems - 1];
        }
    }
}
