using System;
using System.Data.SqlClient;
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

        public double FractionalKnapsackCalculator(long capacityOfKnapsack, ValueAndWeight[] valueWeightArray)
        {
            var numberOfItems = valueWeightArray.Length;
            
            var valueWeightRatioArr = new double[valueWeightArray.Length];

            for (int i = 0; i < valueWeightRatioArr.Length; i++)
            {
                valueWeightRatioArr[i] = (double) valueWeightArray[i].Value / valueWeightArray[i].Weight;
            }

            Array.Reverse(valueWeightRatioArr);

            // fill up the knapsack one fraction at a time using the sorted list

            // base case
            if (numberOfItems == 1)
            {
                return (double)capacityOfKnapsack / valueWeightArray[0].Weight*valueWeightArray[0].Value;
            }

            return 0;
        }

        public class ValueAndWeight
        {
            public long Value { get; set; }
            public long Weight { get; set; }
        }
    }
}
