using System;
using System.Data;
using System.Linq;

namespace FractionalKnapsack
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
            var input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();
            var numItems = input[0];
            var knapsackCapacity = input[1];

            var valueAndWeightInput = new ValueAndWeight[numItems];

            for (int i = 0; i < numItems; i++)
            {
                input = Console.ReadLine().Split(' ').Select(n => Convert.ToInt64(n)).ToArray();

                valueAndWeightInput[i] = new ValueAndWeight
                {
                    Value = input[0],
                    Weight = input[1]
                };
            }

            Console.WriteLine(FractionalKnapsackCalculator(knapsackCapacity, valueAndWeightInput));
        }

        public class ValueAndWeight
        {
            public long Value { get; set; }
            public long Weight { get; set; }
        }

        public double FractionalKnapsackCalculator(long knapsackCapacity, ValueAndWeight[] valueWeightArray)
        {
            var numberOfItems = valueWeightArray.Length;
            
            var descendingValueWeight = valueWeightArray.OrderByDescending(x => x.Value / x.Weight).ToArray();

            if (numberOfItems == 1 || valueWeightArray[0].Weight >= knapsackCapacity)
            {
                return (double)knapsackCapacity / valueWeightArray[0].Weight*valueWeightArray[0].Value;
            }

            var remainingKnapsackCapacity = knapsackCapacity - descendingValueWeight[0].Weight;

            var newLength = numberOfItems - 1;
            var remainingInventoryItems = new ValueAndWeight[numberOfItems-1];
            for (int i = 0; i < newLength; i++)
            {
                remainingInventoryItems[i] = descendingValueWeight[i + 1];
            }

            var frackNap = FractionalKnapsackCalculator(remainingKnapsackCapacity, remainingInventoryItems);

            double optimalKnapsackValue = 0;
            optimalKnapsackValue = optimalKnapsackValue + descendingValueWeight[0].Value + frackNap;

            return optimalKnapsackValue;
        }
    }
}
