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
            var line1Input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            var knapsackCapacity = line1Input[0];
            var line2Input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

            var appendedLine2Input = new int[line2Input.Length + 1];
            line2Input.CopyTo(appendedLine2Input,1);

            var weights = appendedLine2Input;
            var values = appendedLine2Input;
            Console.WriteLine(KnapsackNoRep(knapsackCapacity, weights, values));
            Console.Read();
        }

        public int KnapsackNoRep(int totalKnapsackCapacity, int[] weights, int[] values)
        {


            var numItems = weights.GetLength(0);
            var value = new int[totalKnapsackCapacity+1, numItems];

            for (int itemNumber = 1; itemNumber < numItems; itemNumber++)
            {
                for (int knapsackCapacityIncrementer = 1; knapsackCapacityIncrementer <= totalKnapsackCapacity; knapsackCapacityIncrementer++)
                {
                    value[knapsackCapacityIncrementer, itemNumber] = value[knapsackCapacityIncrementer, itemNumber - 1];

                    var potentialItemWeight = weights[itemNumber];
                    if (potentialItemWeight <= knapsackCapacityIncrementer)
                    {
                        var potentialItemValue = values[itemNumber];
                        var tempValue = value[knapsackCapacityIncrementer - potentialItemWeight, itemNumber - 1] + potentialItemValue;
                        if (value[knapsackCapacityIncrementer, itemNumber] < tempValue)
                        {
                            value[knapsackCapacityIncrementer, itemNumber] = tempValue;
                        }
                    }
                }
            }

            return value[totalKnapsackCapacity, numItems-1];
        }
    }
}
